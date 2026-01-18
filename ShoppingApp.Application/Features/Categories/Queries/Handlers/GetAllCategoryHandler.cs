using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Categories.DTOs;

namespace ShoppingApp.Application.Features.Categories.Queries.Handlers
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>?> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
