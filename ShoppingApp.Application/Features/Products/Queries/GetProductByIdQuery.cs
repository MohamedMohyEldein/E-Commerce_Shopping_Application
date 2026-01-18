using MediatR;
using ShoppingApp.Application.Features.Products.DTOs;

namespace ShoppingApp.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Ulid? Id) : IRequest<ProductDto?>;
}
