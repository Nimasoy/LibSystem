using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Application.Interfaces.Repositories;
using MediatR;

namespace Library.Application.Features.Books.Commands
{
    public record DeleteBookCommand(int Id) : IRequest;

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _repository;

        public DeleteBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.Id);

            if (book == null)
            {
                throw new Exception("Book not found."); 
            }

            await _repository.DeleteAsync(book);

            return Unit.Value;
        }

        Task IRequestHandler<DeleteBookCommand>.Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
