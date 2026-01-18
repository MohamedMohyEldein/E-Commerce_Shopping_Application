using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.ProductVariants.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.ProductVariants.Queries.Handlers
{
    public class GetProductVariantByIdQueryHandler : IRequestHandler<GetProductVariantByIdQuery, ProductVariantDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductVariantByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVariantDto?> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null || request.VariantId is null || request.ProductId is null)
            {
                throw new BadRequestException("Invalid request data.");
            }
            var productVariant = await _unitOfWork.ProductVariantRepository.GetProductVariantByProductIdAsync(request.VariantId, request.ProductId);
            return _mapper.Map<ProductVariantDto?>(productVariant);
        }
    }
}
