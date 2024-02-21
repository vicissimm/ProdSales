using Application.Route;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Application.Handler.ProductHandler.Command;

using Authorization = Infrastructure.Repository.Authorization;


namespace Application.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly bool _adminRole = true;
        public DeleteProductValidator(IProductRepository _repo)
        {
            RuleFor(x => x.Product.Id).Must((x, y) =>
            {
                var toodItem = _repo.GetProductById(y, x.AccessToken);

                return toodItem != null;
            }).WithMessage("Todo item does not exist");

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
