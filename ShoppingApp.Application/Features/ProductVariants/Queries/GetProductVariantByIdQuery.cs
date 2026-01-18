using MediatR;
using ShoppingApp.Application.Features.ProductVariants.DTOs;

namespace ShoppingApp.Application.Features.ProductVariants.Queries
{
    public record GetProductVariantByIdQuery(Ulid? VariantId, Ulid? ProductId) : IRequest<ProductVariantDto?>;
}
