using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperation.Command.CreatBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);//listede aynı isimden veri var mı diye bakıyor.

           if(book is not null)
           {
               throw new InvalidOperationException("Kitap Zaten Mevcut");
           }
           book= _mapper.Map<Book>(Model); // bu sayede asagıdaki kod satırlarını yazmaya gerek kalmıyor //new Book();
           /*book.Title=Model.Title;
           book.PublishDate=Model.PublishDate;
           book.PageCount=Model.PageCount;
           book.GenreId=Model.GenreId;*/
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();//kaydetme işlemi için, save etmek       
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }

    }
}