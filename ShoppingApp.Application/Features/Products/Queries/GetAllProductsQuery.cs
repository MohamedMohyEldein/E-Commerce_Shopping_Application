using MediatR;
using ShoppingApp.Application.Features.Products.DTOs;

namespace ShoppingApp.Application.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>?>;
}
