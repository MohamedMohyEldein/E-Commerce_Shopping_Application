using MediatR;
using ShoppingApp.Application.Features.Categories.DTOs;

namespace ShoppingApp.Application.Features.Categories.Queries
{
    public record GetAllCategoriesQuery : IRequest<List<CategoryDto>?>;
}
