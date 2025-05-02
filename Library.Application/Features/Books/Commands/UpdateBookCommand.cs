using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.DTOs.Book;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest
    {
        public UpdateBookDto UpdateBookDto { get; set; }

        public UpdateBookCommand(UpdateBookDto updateBookDto)
        {
            UpdateBookDto = updateBookDto;
        }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _repository.GetByIdAsync(request.UpdateBookDto.Id);

            if (existingBook == null)
            {
                throw new Exception("Book not found.");
            }

            _mapper.Map(request.UpdateBookDto, existingBook);

            await _repository.UpdateAsync(existingBook);

            return Unit.Value;
        }

        Task IRequestHandler<UpdateBookCommand>.Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
