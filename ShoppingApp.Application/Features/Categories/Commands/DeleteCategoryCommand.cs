using MediatR;

namespace ShoppingApp.Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(Ulid? Id) : IRequest<bool>;
}
