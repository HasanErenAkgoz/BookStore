using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors(){
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);            
        }

        [HttpGet("{id}")]
        public ActionResult GetAuthorsById(int id){
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public ActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor){
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            command.Model = newAuthor;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id){
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            command.AuthorId = id;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAuthor([FromBody] UpdateAuthorModel updatedAuthor, int id){
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            command.AuthorId = id;
            command.Model = updatedAuthor;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

    }
}