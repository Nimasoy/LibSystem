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

namespace Library.Application.Features.Books.Queries
{
    public record GetAllBooksQuery : IRequest<List<BookDto>>;

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync();
            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return bookDtos;
        }
    }
}
