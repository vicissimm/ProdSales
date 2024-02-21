using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repository
{
    public class Authorization
    {
        private readonly IConfiguration _config;

        public Authorization(IConfiguration config)
        {
            _config = config;
        }

        public bool IsValidRefreshToken(string token)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurelyRefresh";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://mysite.com";
            var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool IsValidToken(string token)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurely";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://mysite.com";
            var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void RefreshToken(int userId, bool isAdmin, out string refreshToken)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurelyRefresh";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://mysite.com";
            var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, isAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            refreshToken = tokenHandler.WriteToken(token);
        }

        public void Token(int userId, bool isAdmin, out string accessToken)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurely";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://mysite.com";
            var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, isAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            accessToken = tokenHandler.WriteToken(token);
        }

        public UserObject DecodeRefreshToken(string refreshToken)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurelyRefresh";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var handler = new JwtSecurityTokenHandler();

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = mySecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(refreshToken, validations, out var tokenSecure);

            int userid = int.Parse(claims.Claims.First(x => true).Value);
            bool isAdmin = bool.Parse(claims.Claims.ElementAt(1).Value);

            return new UserObject
            {
                Id = userid,
                IsAdmin = isAdmin
            };
        }

        public UserObject DecodeAccessToken(string accessToken)
        {
            var mySecret = "ForTheLoveOfGodStoreAndLeadThisSecurely";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var handler = new JwtSecurityTokenHandler();

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = mySecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(accessToken, validations, out var tokenSecure);

            int userid = int.Parse(claims.Claims.First(x => true).Value);
            bool isAdmin = bool.Parse(claims.Claims.ElementAt(1).Value);



            return new UserObject
            {
                Id = userid,
                IsAdmin = isAdmin,
            };
        }
    }
}