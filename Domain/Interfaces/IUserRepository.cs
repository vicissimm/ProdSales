using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);
        public Task<User> CheckEmail (string email);
        public Task<TokenObject> Login (string email, string password);

    }
}
