using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle(){
            var author = _context.Authors.SingleOrDefault(x => x.IsActive && x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");
            
            return _mapper.Map<AuthorDetailViewModel>(author);
        }

    }

    public class AuthorDetailViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}