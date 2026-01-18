using FluentValidation;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(p => p.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to zero.");
        }
    }
}
