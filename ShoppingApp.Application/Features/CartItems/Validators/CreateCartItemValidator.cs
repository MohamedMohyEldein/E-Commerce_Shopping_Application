using FluentValidation;
using ShoppingApp.Application.Features.CartItems.DTOs;

namespace ShoppingApp.Application.Features.CartItems.Validators
{
    public class CreateCartItemValidator : AbstractValidator<CreateCartItemDto>
    {
        public CreateCartItemValidator()
        {
            RuleFor(p => p.CartId).NotEmpty().WithMessage("CartId is required.");
            RuleFor(p => p.ProductVariantId).NotEmpty().WithMessage("ProductVariantId is required.");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("Quantity is required.").GreaterThan(0).WithMessage("Quantity should be greater than 0.");
        }
    }
}
