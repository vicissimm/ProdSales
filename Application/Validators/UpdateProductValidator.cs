//using Application.Route;
//using Domain.Interfaces;
//using FluentValidation;
//using Microsoft.Extensions.Configuration;
//using Application.Handler.ProductHandler.Command;

//using Authorization = Infrastructure.Repository.Authorization;



//namespace Application.Validators
//{
//    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
//    {
//        private readonly bool _adminRole = true;
//        public UpdateProductValidator(IProductRepository _repo)
//        {

//            RuleFor(x => x.Product.Id).Must((x, y) =>
//            {
//                var todoitem = _repo.GetProductById(y, x.AccessToken).Result;

//                return todoitem != null;

//            }).WithMessage("Todo item doesn't exist");

//            RuleFor(x => x.AccessToken)
//                .Must((x, y) =>
//                {
//                    var role = GetRoleFromToken(y);

//                    return BeAdmin(role);
//                })
//                .WithMessage("You don't have permission to add product");
//        }
//        private bool BeAdmin(bool role)
//        {
//            return role == _adminRole;
//        }
//        private bool GetRoleFromToken(string accessToken)
//        {
//            var config = new ConfigurationManager();
//            var auth = new Authorization(config);
//            var userObj = auth.DecodeAccessToken(accessToken);

//            return userObj.IsAdmin;

//        }
//    }
//}
