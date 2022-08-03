using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(coomand => coomand.Model.GenreId).GreaterThan(0);
            RuleFor(coomand => coomand.Model.Title).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
