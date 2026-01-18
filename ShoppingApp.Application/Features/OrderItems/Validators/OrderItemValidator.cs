using FluentValidation;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Order Item ID must not be empty.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.").NotEmpty().WithMessage("Quantity must not be empty.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.").NotEmpty().WithMessage("Unit price must not be empty.");
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID must not be empty.");
            RuleFor(x => x.ProductVariantId)
                .NotEmpty().WithMessage("Product Variant ID must not be empty.");
        }
    }
}
