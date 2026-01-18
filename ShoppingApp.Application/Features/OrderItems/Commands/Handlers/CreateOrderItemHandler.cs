using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.OrderItems.Commands.Handlers
{
    public class CreateOrderItemHandler : IRequestHandler<CreateOrderItemcommand, OrderItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderItemDto?> Handle(CreateOrderItemcommand request, CancellationToken cancellationToken)
        {
            if (request is null || request.OrderItemDto is null)
            {
                throw new BadRequestException("Order item can't be null");
            }
            var orderItem = await _unitOfWork.OrderItemRepository.GetOrderAndVariantByIdAsync(request.OrderItemDto.OrderId, request.OrderItemDto.ProductVariantId);

            if (orderItem is null)
            {
                throw new NotFoundException($"OrderItem not found.");
            }

            orderItem = _mapper.Map<OrderItem>(request.OrderItemDto);
            orderItem.Id = Ulid.NewUlid();
            await _unitOfWork.OrderItems.AddAsync(orderItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderItemDto>(orderItem);
        }
    }
}
