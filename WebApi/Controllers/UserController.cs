using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.TokenOperations.Models;
using WebApi.Application.UserOperation.Commands;
using WebApi.Application.UserOperations.Commands;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : Controller
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(BookStoreDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel createUserModel)
        {
            CreateUserCommand command = new(_context, _mapper);
            command.Model = createUserModel;
            command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new(_context, _mapper,_configuration);
            command.model = login;
            var token = command.Handle();
            return token;
        }
    }
}
