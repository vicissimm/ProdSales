using Application.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Application.Handler.ProductHandler.Command;

using Authorization = Infrastructure.Repository.Authorization;

namespace Application.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        private readonly bool _adminRole = true;
        public AddProductValidator()
        {

            RuleFor(x => x.Product.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .NotNull()
                .WithMessage("Name is required");
            RuleFor(x => x.AccessToken)
                .Must((x, y) =>
                {
                    var role = GetRoleFromToken(y);

                    return BeAdmin(role);
                })
                .WithMessage("You don't have permission to add product");
        }

        private bool BeAdmin(bool role)
        {
            return role == _adminRole;
        }
        private bool GetRoleFromToken(string accessToken)
        {
            var config = new ConfigurationManager();
            var auth = new Authorization(config);
            var userObj = auth.DecodeAccessToken(accessToken);

            return userObj.IsAdmin;

        }
    }
}
