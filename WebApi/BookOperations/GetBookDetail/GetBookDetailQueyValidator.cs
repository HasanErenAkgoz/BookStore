using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueyValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueyValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
