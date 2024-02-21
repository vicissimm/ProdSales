using Application.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserDto>
    {
        public LoginValidator(IProdSalesDataContext _context, IPasswordHasher _passwordHasher)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .NotNull()
                .WithMessage("Email is required.")
                .WithMessage("Email does not exist");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .NotNull()
                .WithMessage("Password is required.")
                .Must((x, y) =>
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == x.Email);

                    if (user == null)
                    {
                        return false;
                    }

                    return _passwordHasher.VerifyPassword(user.Password, y);
                })
                .WithMessage("Incorrect password");
        }
    }
}
