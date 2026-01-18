using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Categories.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Categories.Queries.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.id is null)
            {
                throw new BadRequestException("Category id cannot be null.");
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(request.id.Value);

            if(category is null)
            {
                throw new NotFoundException($"Categroy with id = {request.id} not found.");
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
