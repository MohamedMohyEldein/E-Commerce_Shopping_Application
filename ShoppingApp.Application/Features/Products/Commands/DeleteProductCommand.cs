using MediatR;

namespace ShoppingApp.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Ulid? Id) : IRequest<bool>;

}
