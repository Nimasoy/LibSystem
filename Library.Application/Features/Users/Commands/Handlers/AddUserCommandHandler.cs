using MediatR;
using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Application.Common; // Added for BaseResponse
using Microsoft.Extensions.Logging; // Added for logging

namespace Library.Application.Features.Users.Commands.Handlers
{
    // Changed return type to BaseResponse<int>
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, BaseResponse<int>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserCommandHandler> _logger; // Added logger

        public AddUserCommandHandler(IUserRepository repository, IMapper mapper, ILogger<AddUserCommandHandler> logger) // Added logger
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger; // Added logger
        }

        // Changed return type to Task<BaseResponse<int>>
        public async Task<BaseResponse<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request.CreateUserDto);

                // Add validation logic here if needed before adding

                await _repository.AddAsync(user);

                _logger.LogInformation("User {UserId} created successfully.", user.Id);

                // Return success response with the new user ID
                return new BaseResponse<int> { Success = true, Data = user.Id, Message = "User created successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");
                // Return failure response
                return new BaseResponse<int> { Success = false, Message = "An error occurred while creating the user." };
            }
        } 
    }
}