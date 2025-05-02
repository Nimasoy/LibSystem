using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Application.DTOs.User;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Common;

namespace Library.Application.Features.Users.Commands
{
    public record UpdateUserCommand(UpdateUserDto UpdateUserDto) : IRequest<BaseResponse>;

    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
