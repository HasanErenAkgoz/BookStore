using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand{
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(command => command.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");
            var book = _context.Books.SingleOrDefault(command => command.AuthorId == AuthorId);
            if(book is not null)
                throw new InvalidOperationException("Yazara ait kitap bulunmaktadır. Önce kitabı siliniz!");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}