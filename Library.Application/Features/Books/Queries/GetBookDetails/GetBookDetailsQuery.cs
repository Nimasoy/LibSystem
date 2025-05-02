using Library.Application.Common;
using Library.Application.Features.Books.Queries.GetBookDetails; // Added potentially missing Dto namespace
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBookDetails;

public record GetBookDetailsQuery(int BookId) : IRequest<BaseResponse<BookDetailsDto>>;