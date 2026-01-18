using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.OrderItems.Queries.Handlers
{
    public class GetOrderItemByIdHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderItemByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderItemDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.orderItemId is null || request.orderId is null || request.productVariantId is null)
            {
                throw new BadRequestException("OrderItemId, OrderId and ProductVariantId must be provided.");
            }
            var orderItem = await _unitOfWork.OrderItemRepository.GetOrderItemByIdAsync(request.orderItemId, request.orderId, request.productVariantId);

            if (orderItem is null)
            {
                throw new NotFoundException("OrderItem not found.");
            }
            return _mapper.Map<OrderItemDto>(orderItem);
        }
    }
}
