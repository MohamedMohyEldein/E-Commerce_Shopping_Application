using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.OrderItems.Queries.Handlers
{
    public class GetAllOrderItemsHandler : IRequestHandler<GetAllOrderItemsQuery, List<OrderItemDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrderItemsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderItemDto>?> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
        {
            if(request.orderId is null)
            {
                throw new NotFoundException("OrderId is required to fetch order items.");
            }

            var orderItems = await _unitOfWork.OrderItemRepository.GetAllOrderItemsAsync(request.orderId);

            return orderItems;
        }
    }
}
