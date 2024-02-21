using Convey.CQRS.Commands;

namespace Application.Dto
{
    public class UserDto : ICommand
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}
