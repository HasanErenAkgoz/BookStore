using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand{
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model;

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(command => command.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");
            author.Name = Model.Name != string.Empty ? Model.Name : author.Name;
            author.Surname = Model.Surname != string.Empty ? Model.Surname : author.Surname;
            author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;
            author.IsActive = Model.IsActive;

            _context.SaveChanges();
        }
    }


    public class UpdateAuthorModel{
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; } = true;
    }
}