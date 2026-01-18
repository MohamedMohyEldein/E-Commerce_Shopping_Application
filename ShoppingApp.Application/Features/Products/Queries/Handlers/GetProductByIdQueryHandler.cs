using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Products.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Products.Queries.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new BadRequestException("Request cannot be null.");
            }
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id.Value);
            if(product is null)
            {
                throw new NotFoundException($"Product with Id = {request.Id.Value} is not found");
            }
            return _mapper.Map<ProductDto?>(product);
        }
    }
}
