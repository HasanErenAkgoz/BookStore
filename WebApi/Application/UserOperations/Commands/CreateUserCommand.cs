using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperation.Commands
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);//listede aynı isimden veri var mı diye bakıyor.

            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı Zaten Mevcut");
            }
            user = _mapper.Map<User>(Model); // bu sayede asagıdaki kod satırlarını yazmaya gerek kalmıyor //new Book();
            /*book.Title=Model.Title;
            book.PublishDate=Model.PublishDate;
            book.PageCount=Model.PageCount;
            book.GenreId=Model.GenreId;*/
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();//kaydetme işlemi için, save etmek       
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}