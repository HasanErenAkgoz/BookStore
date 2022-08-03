using FluentValidation;
using WebApi.BookOperations.CreateBook;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
