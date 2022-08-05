using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model;

        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(command => command.Name == Model.Name);
            if(author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut!");
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

    }

    public class CreateAuthorModel{
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}