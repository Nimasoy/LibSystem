using FluentValidation;

namespace Library.Application.Features.Books.Commands.ReserveBook;

public class ReserveBookCommandValidator : AbstractValidator<ReserveBookCommand>
{
    public ReserveBookCommandValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0)
            .WithMessage("Book ID must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID must be greater than 0");
    }
} 