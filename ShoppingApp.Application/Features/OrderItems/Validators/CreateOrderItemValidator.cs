using FluentValidation;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Validators
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.").NotEmpty().WithMessage("Quantity must not be empty.");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID must not be empty.");
            RuleFor(x => x.ProductVariantId)
                .NotEmpty().WithMessage("Product Variant ID must not be empty.");
        }
    }
}
