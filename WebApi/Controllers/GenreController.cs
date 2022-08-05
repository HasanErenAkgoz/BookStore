using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres(){
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public ActionResult GetGenresById(int id){
            GenreDetailViewModel result;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre){
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Hanlde();
            return Ok();
        }

        //Çalışmıyor kontrol et
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre){
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            command.GenreId = id;
            command.Model = updatedGenre;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}