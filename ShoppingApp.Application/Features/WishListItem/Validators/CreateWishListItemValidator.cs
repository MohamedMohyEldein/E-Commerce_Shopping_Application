using FluentValidation;
using ShoppingApp.Application.Features.WishListItem.DTOs;

namespace ShoppingApp.Application.Features.WishListItem.Validators
{
    public class CreateWishListItemValidator : AbstractValidator<CreateWishListItemDto>
    {
        public CreateWishListItemValidator()
        {
            RuleFor(p => p.WishlistId).NotEmpty().WithMessage("WishListId is required.");
            RuleFor(p => p.ProductVariantId).NotEmpty().WithMessage("ProductVariantId is required.");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("Quantity is required.").GreaterThan(0).WithMessage("Quantity should be greater than 0.");
        }
    }
}
