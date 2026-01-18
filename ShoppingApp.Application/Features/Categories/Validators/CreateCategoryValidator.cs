using FluentValidation;
using ShoppingApp.Application.Features.Categories.DTOs;

namespace ShoppingApp.Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Image).NotEmpty().WithMessage("Category image is required.");
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
