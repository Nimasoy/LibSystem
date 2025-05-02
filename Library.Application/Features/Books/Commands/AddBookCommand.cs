using Library.Domain.Interfaces;
using Library.Application.DTOs.Book;
using FluentValidation;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.CreateBookDto.Title).NotEmpty();
            RuleFor(x => x.CreateBookDto.Author).NotEmpty();
            RuleFor(x => x.CreateBookDto.ISBN).NotEmpty();
            RuleFor(x => x.CreateBookDto.Publisher).NotEmpty();
            RuleFor(x => x.CreateBookDto.Year).InclusiveBetween(1000, DateTime.UtcNow.Year);
            RuleFor(x => x.CreateBookDto.TotalCopies).GreaterThan(0);
            RuleFor(x => x.CreateBookDto.CategoryId).GreaterThan(0);
        }
    }

    public class AddBookCommand : IRequest<int>
    {
        public CreateBookDto CreateBookDto { get; set; }

        public AddBookCommand(CreateBookDto createBookDto)
        {
            CreateBookDto = createBookDto;
        }
    }
}

