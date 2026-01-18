using FluentValidation;
using ShoppingApp.Application.Features.Products.DTOs;

namespace ShoppingApp.Application.Features.Products.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Image).NotEmpty().WithMessage("Product image is required.");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.")
            .MinimumLength(2).WithMessage("Product name must be at least 2 characters long.");
            RuleFor(p => p.Description).MaximumLength(1000).WithMessage("Product description must not exceed 1000 characters.")
                .NotEmpty().WithMessage("Product description is required.");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Product price must be greater than or equal to 0.").NotEmpty().WithMessage("Price is required");
            RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0.").NotEmpty().WithMessage("Stock quantity is required");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Category ID is required.");
        }
    }
}
