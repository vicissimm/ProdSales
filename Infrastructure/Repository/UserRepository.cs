using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProdSalesContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(ProdSalesContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CheckEmail(string email)
        {
            var isValid = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return isValid;
        }

        public async Task CreateUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<TokenObject> Login(string email, string password)
        {
            var configurationManager = new ConfigurationManager();
            var auth = new Authorization(configurationManager);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            var accessToken = string.Empty;
            var refreshToken = string.Empty;

            auth.Token(user.Id, user.IsAdmin, out accessToken);
            auth.RefreshToken(user.Id, user.IsAdmin, out refreshToken);

            var tokens = new TokenObject()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return tokens;

        }
    }
}
