using MediatR;

namespace ShoppingApp.Application.Features.ProductVariants.Commands
{
    public record DeleteProductVariantCommand(Ulid? Id) : IRequest<bool>;
}
