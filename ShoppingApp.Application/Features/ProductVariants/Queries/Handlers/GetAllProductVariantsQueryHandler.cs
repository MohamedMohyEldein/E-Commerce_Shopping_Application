using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.ProductVariants.DTOs;

namespace ShoppingApp.Application.Features.ProductVariants.Queries.Handlers
{
    public class GetAllProductVariantsQueryHandler : IRequestHandler<GetAllProductVariantsQuery, List<ProductVariantDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductVariantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductVariantDto>?> Handle(GetAllProductVariantsQuery request, CancellationToken cancellationToken)
        {
            var productVariants = await _unitOfWork.ProductVariantRepository.GetAllProductVariantsByProductIdAsync(request.ProductId);
            return _mapper.Map<List<ProductVariantDto>?>(productVariants);
        }
    }
}
