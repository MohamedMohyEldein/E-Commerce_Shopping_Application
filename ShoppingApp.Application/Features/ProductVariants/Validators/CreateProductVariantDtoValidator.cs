using FluentValidation;
using ShoppingApp.Application.Features.ProductVariants.DTOs;

namespace ShoppingApp.Application.Features.ProductVariants.Validators
{
    public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
    {
        public CreateProductVariantDtoValidator()
        {
            RuleFor(p => p.Image).NotEmpty().WithMessage("Product variant image is required.");
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(p => p.Size).MinimumLength(1).WithMessage("Variant size should be greater than 0").NotEmpty().WithMessage("Variant size is required.")
                .MaximumLength(50).WithMessage("Variant size must not exceed 50 characters.");
            RuleFor(p => p.Color).MinimumLength(1).NotEmpty().WithMessage("Variant color is required.")
                .MaximumLength(50).WithMessage("Variant color must not exceed 50 characters.");
            RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0.")
                .NotEmpty().WithMessage("Stock quantity is required.");
        }
    }
}
