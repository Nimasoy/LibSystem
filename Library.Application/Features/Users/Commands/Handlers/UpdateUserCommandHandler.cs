using AutoMapper;
using Library.Application.Common;
using Library.Application.Features.Users.Commands;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Users.Commands.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        IUserRepository repository, 
        IMapper mapper,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _repository.GetByIdAsync(request.UpdateUserDto.Id);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", request.UpdateUserDto.Id);
                return new BaseResponse { Success = false, Message = "User not found" };
            }

            _mapper.Map(request.UpdateUserDto, existingUser);
            await _repository.UpdateAsync(existingUser);

            _logger.LogInformation("User {UserId} updated successfully", request.UpdateUserDto.Id);
            return new BaseResponse { Success = true, Message = "User updated successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", request.UpdateUserDto.Id);
            return new BaseResponse { Success = false, Message = "An error occurred while updating the user" };
        }
    }
}
