using Convey.CQRS.Queries;

namespace Application.Dto
{
    public class LoginUserDto : IQuery<TokenDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
