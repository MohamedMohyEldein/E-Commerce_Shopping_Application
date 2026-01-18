using MediatR;
using ShoppingApp.Application.Features.ProductVariants.DTOs;

namespace ShoppingApp.Application.Features.ProductVariants.Queries
{
    public record GetAllProductVariantsQuery(Ulid ProductId) : IRequest<List<ProductVariantDto>?>;
}
