//using AutoMapper;
//using MediatR;
//using ShoppingApp.Application.Features.OrderItems.DTOs;
//using ShoppingApp.Domain.Exceptions;

//namespace ShoppingApp.Application.Features.OrderItems.Commands.Handlers
//{
//    public class UpdateOrderItemHandler : IRequestHandler<UpdateOrderItemcommand, OrderItemDto?>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public UpdateOrderItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public async Task<OrderItemDto?> Handle(UpdateOrderItemcommand request, CancellationToken cancellationToken)
//        {
//            if (request is null || request.OrderItemDto is null)
//            {
//                throw new BadRequestException("UpdateOrderItemCommand can't be null.");
//            }

//            var orderItem = await _unitOfWork.OrderItemRepository.GetOrderItemByIdAsync(request.orderItemId, request.OrderItemDto.OrderId, request.OrderItemDto.ProductVariantId);

//            if (orderItem is null)
//            {
//                throw new NotFoundException($"OrderItem not found.");
//            }

//            orderItem.Quantity = request.OrderItemDto.Quantity;
//            _unitOfWork.OrderItems.Update(orderItem);
//            await _unitOfWork.SaveChangesAsync(cancellationToken);
//            return _mapper.Map<OrderItemDto>(orderItem);
//        }
//    }
//}
