using Application.Dto;
using Convey.CQRS.Queries;
using Domain.Interfaces;
using AutoMapper;

namespace Application.Handler.UserHandler.Query
{
    public class Login : IQueryHandler<LoginUserDto, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Login(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TokenDto> HandleAsync(LoginUserDto query, CancellationToken cancellationToken = default)
        {
            var tokens = await _userRepository.Login(query.Email, query.Password);
            var tokenDto = _mapper.Map<TokenDto>(tokens);

            return tokenDto;
        }
    }
}
