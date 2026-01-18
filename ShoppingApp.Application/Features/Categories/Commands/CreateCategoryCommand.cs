using MediatR;
using ShoppingApp.Application.Features.Categories.DTOs;

namespace ShoppingApp.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand(CreateCategoryDto? CreateCategoryDto) : IRequest<CategoryDto>;
}
