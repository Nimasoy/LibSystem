using AutoMapper;
using Library.Application.Features.Books.Commands;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, int>
{
    private readonly IBookRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IBookRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request.CreateBookDto);
        var dto = request.CreateBookDto;

        if (!await _categoryRepository.ExistsAsync(dto.CategoryId))
            throw new Exception("Category does not exist.");

        if (await _repository.ExistsAsync(dto.ISBN))
            throw new Exception("A book with this ISBN already exists.");
        await _repository.AddAsync(book);
        return book.Id;
    }
}