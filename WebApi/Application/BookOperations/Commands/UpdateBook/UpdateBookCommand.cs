using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand{
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel Model {get; set;}
        public UpdateBookCommand(BookStoreDbContext dbContext){
            _dbContext= dbContext;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null){
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}