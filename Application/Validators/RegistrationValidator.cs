using Application.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class RegistrationValidator : AbstractValidator<UserDto>
    {
        private readonly IProdSalesDataContext _context;
        public RegistrationValidator(IProdSalesDataContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .NotNull()
                .WithMessage("Name is required");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .NotNull()
                .WithMessage("Email is required.")
                .Must(BeUniqueEmail)
                .WithMessage("This email is already in use.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .NotNull()
                .WithMessage("Password is required.");
        }

        private bool BeUniqueEmail(string email)
        {
            return !_context.Users.Any(u => u.Email == email);
        }
    }
}
