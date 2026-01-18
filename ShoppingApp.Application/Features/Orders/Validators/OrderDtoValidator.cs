using FluentValidation;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(p => p.Status).NotEmpty().WithMessage("Status is required.").DependentRules(() => 
            {
                RuleFor(p => p.Status)
                    .IsInEnum().WithMessage("Status must be a valid enum value.");
            });
            RuleFor(p => p.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to zero.");
            RuleFor(p => p.OrderDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("OrderDate cannot be in the future.");


        }
    }
}
