using AutoMapper;
using Library.Application.Common;
using Library.Application.Interfaces; // Assuming ICacheService will be moved/defined here
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
//using Library.Infrastructure.Services; // Avoid direct Infrastructure dependency if possible

namespace Library.Application.Features.Books.Queries.GetBookDetails;

public class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BaseResponse<BookDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBookDetailsQueryHandler> _logger;
    private readonly ICacheService _cacheService;

    public GetBookDetailsQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        ILogger<GetBookDetailsQueryHandler> logger,
        ICacheService cacheService)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<BookDetailsDto>> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cacheKey = $"book_details_{request.BookId}";
            var cachedBook = _cacheService.Get<BookDetailsDto>(cacheKey);

            if (cachedBook != null)
            {
                return new BaseResponse<BookDetailsDto> { Success = true, Data = cachedBook };
            }

            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return new BaseResponse<BookDetailsDto> { Success = false, Message = "Book not found" };

            var bookDto = _mapper.Map<BookDetailsDto>(book);
            _cacheService.Set(cacheKey, bookDto);

            return new BaseResponse<BookDetailsDto> { Success = true, Data = bookDto };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting book details for book {BookId}", request.BookId);
            return new BaseResponse<BookDetailsDto> { Success = false, Message = "An error occurred while getting book details" };
        }
    }
} 