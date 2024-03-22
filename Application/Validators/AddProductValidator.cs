
//using FluentValidation;
//using Microsoft.Extensions.Configuration;
//using Application.Handler.ProductHandler.Command;

//using Authorization = Infrastructure.Repository.Authorization;
//using Microsoft.AspNetCore.RequestDecompression;
//using Microsoft.AspNetCore.Http;

//namespace Application.Validators
//{
//    public class AddProductValidator : AbstractValidator<AddProductCommand>
//    {
//        private readonly HttpContext _httpContext;
//        public AddProductValidator(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContext = httpContextAccessor.HttpContext;

//            RuleFor(x => x.Product.Name)
//                .NotEmpty()
//                .WithMessage("Name is required")
//                .NotNull()
//                .WithMessage("Name is required");
//            RuleFor(x => x.IsAdmin)
//                .Must(BeAdmin)
//                .WithMessage("You are not authorized to perform this action."); 
//        }

//        private bool BeAdmin(bool isAdmin)
//        {
//            // Check if the user is an admin based on the HttpContext
//            var isAdminFromContext = _httpContext.Items["IsAdmin"] as bool?;
//            return isAdminFromContext == true; // Only allow if the user is an admin
//        }
//    }
//}
