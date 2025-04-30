using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;
using Library.Application.Interfaces.Repositories;
using MediatR;
using AutoMapper;
using Library.Application.DTOs.Book;

namespace Library.Application.Features.Books.Commands
{
    public class AddBookCommand : IRequest<int>
    {
        public CreateBookDto CreateBookDto { get; set; }

        public AddBookCommand(CreateBookDto createBookDto)
        {
            CreateBookDto = createBookDto;
        }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, int>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request.CreateBookDto);

            await _repository.AddAsync(book);
            return book.Id;
        }
    }
}

