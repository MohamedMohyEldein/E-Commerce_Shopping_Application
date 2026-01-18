using MediatR;
using ShoppingApp.Application.Features.Products.DTOs;

namespace ShoppingApp.Application.Features.Products.Commands
{
    public record CreateProductCommand(CreateProductDto CreateProductDto) : IRequest<ProductDto>;

}
