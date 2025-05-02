using Library.Application.Common;
using Library.Application.DTOs.User;
using MediatR;

namespace Library.Application.Features.Users.Commands
{
    // Changed to return the ID of the created user within BaseResponse
    public record AddUserCommand(CreateUserDto CreateUserDto) : IRequest<BaseResponse<int>>;
}