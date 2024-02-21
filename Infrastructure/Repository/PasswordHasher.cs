using System.Security.Cryptography;
using Domain.Interfaces;

namespace Infrastructure.Repository
{

    public class PasswordHasher : IPasswordHasher
    {

        private const int _saltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(_saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithmName, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}