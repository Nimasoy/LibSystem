using FluentValidation;

namespace Library.Application.Features.Books.Commands.ReturnBook;

public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
{
    public ReturnBookCommandValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0)
            .WithMessage("Book ID must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID must be greater than 0");
    }
} 