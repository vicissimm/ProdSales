using Application.Route;
using Domain.Interfaces;
using FluentValidation;
using Application.Handler.ProductHandler.Query;

namespace Application.Validators
{
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductValidator(IProductRepository _repo)
        {
            RuleFor(x => x.Id).Must((x, y) =>
            {
                var todoitem = _repo.GetProductById(y,x.AccessToken).Result;

                return todoitem != null;

            }).WithMessage("Todo item doesn't exist");
        }
    }
}
