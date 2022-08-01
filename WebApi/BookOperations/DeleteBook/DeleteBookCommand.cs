using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.CreateBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public int BookId {get; set;}
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id ==BookId);

          if(book is null)
            throw new InvalidOperationException("Silinecek kitap bulunamadı!");
           

          _dbContext.Books.Remove(book);
          _dbContext.SaveChanges();
            
        }
    }
}