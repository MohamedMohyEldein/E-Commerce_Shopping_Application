using MediatR;
using ShoppingApp.Application.Features.Products.DTOs;

namespace ShoppingApp.Application.Features.Products.Commands
{
    public record UpdateProductCommand(Ulid? Id, UpdateProductDto? UpdateProductDto) : IRequest<ProductDto>;

}
