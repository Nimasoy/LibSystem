using Library.Domain.Interfaces;
using MediatR;
using AutoMapper;
using Library.Application.DTOs.User;

namespace Library.Application.Features.Users.Queries
{
    public record GetAllUsersQuery : IRequest<List<UserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }
    }
}
