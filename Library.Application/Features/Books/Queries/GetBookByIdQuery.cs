using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.DTOs.Book;
using Library.Application.Interfaces.Repositories;
using MediatR;

namespace Library.Application.Features.Books.Queries
{
    public record GetBookByIdQuery(int Id) : IRequest<BookDto>;

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            return _mapper.Map<BookDto>(book);
        }
    }
}
