using Convey.CQRS.Queries;

namespace Application.Dto
{
    public class TokenDto : IQuery
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}