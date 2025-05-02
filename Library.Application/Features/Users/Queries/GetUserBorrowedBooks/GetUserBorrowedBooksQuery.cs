using AutoMapper;
using Library.Application.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Users.Queries.GetUserBorrowedBooks;

public record GetUserBorrowedBooksQuery(int UserId) : IRequest<BaseResponse<List<BorrowedBookDto>>>;

public class GetUserBorrowedBooksQueryHandler : IRequestHandler<GetUserBorrowedBooksQuery, BaseResponse<List<BorrowedBookDto>>>
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserBorrowedBooksQueryHandler> _logger;

    public GetUserBorrowedBooksQueryHandler(
        IBorrowRecordRepository borrowRecordRepository,
        IMapper mapper,
        ILogger<GetUserBorrowedBooksQueryHandler> logger)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<List<BorrowedBookDto>>> Handle(GetUserBorrowedBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var borrowRecords = await _borrowRecordRepository.GetActiveBorrowsForUser(request.UserId);
            var borrowedBooks = _mapper.Map<List<BorrowedBookDto>>(borrowRecords);

            return new BaseResponse<List<BorrowedBookDto>>
            {
                Success = true,
                Data = borrowedBooks
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting borrowed books for user {UserId}", request.UserId);
            return new BaseResponse<List<BorrowedBookDto>>
            {
                Success = false,
                Message = "An error occurred while getting borrowed books"
            };
        }
    }
} 