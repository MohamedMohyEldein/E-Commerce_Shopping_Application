using MediatR;
using ShoppingApp.Application.Features.ProductVariants.DTOs;

namespace ShoppingApp.Application.Features.ProductVariants.Commands
{
    public record UpdateProductVariantCommand(UpdateProductVariantDto updateProductVariant) : IRequest<ProductVariantDto>;
}
