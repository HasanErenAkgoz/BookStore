using FluentValidation;
using System;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(coomand => coomand.Model.GenreId).GreaterThan(0);
            RuleFor(coomand => coomand.Model.PageCount).GreaterThan(0);
            RuleFor(coomand => coomand.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(coomand => coomand.Model.Title).NotEmpty();
        }
    }
}
