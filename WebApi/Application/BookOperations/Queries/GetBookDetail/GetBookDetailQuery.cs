using WebApi.DBOperations;
using WebApi;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery{
            private readonly BookStoreDbContext _dbContext;
            private readonly IMapper _mapper;
            public int BookId {get; set;}

        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle(){
                var book = _dbContext.Books.Include(x => x.Genre).Where(book=> book.Id == BookId).SingleOrDefault();
                if(book is null){
                    throw new InvalidOperationException("Kitap bulunamadı!");
                }
                BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);//new BookDetailViewModel();
                // vm.Title = book.Title;
                // vm.PageCount = book.PageCount;
                // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
                // vm.Genre = ((GenreEnum)book.GenreId).ToString();
                return vm;
    }

    }

    public class BookDetailViewModel{
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

    
}