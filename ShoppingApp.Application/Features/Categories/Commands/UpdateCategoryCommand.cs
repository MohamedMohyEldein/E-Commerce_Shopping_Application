using MediatR;
using ShoppingApp.Application.Features.Categories.DTOs;

namespace ShoppingApp.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(Ulid? Id, UpdateCategoryDto UpdateCategoryDto) : IRequest<CategoryDto?>;
}
