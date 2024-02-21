using AutoMapper;
using Application.Dto;
using Convey.CQRS.Commands;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Handler.UserHandler.Command
{
    public class Registration : ICommandHandler<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public Registration(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }
        public async Task HandleAsync(UserDto command, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.CreateUser(user);

        }
    }
}
