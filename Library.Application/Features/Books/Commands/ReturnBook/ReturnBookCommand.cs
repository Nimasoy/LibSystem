using Library.Application.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Library.Application.Common; // Keep BaseResponse using

namespace Library.Application.Features.Books.Commands.ReturnBook;

public record ReturnBookCommand(int BookId, int UserId) : IRequest<BaseResponse>;