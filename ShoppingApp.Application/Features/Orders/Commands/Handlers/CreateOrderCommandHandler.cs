using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Enums;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Orders.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if(request is null || request.Order is null)
            {
                throw new BadRequestException("Order data cannot be null.");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(request.Order!.UserId);

            if (user is null)
            {
                throw new NotFoundException($"User with ID {request.Order.UserId} not found.");
            }
            var order = _mapper.Map<Order>(request.Order);
            
            order.Id = Ulid.NewUlid();
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            order.UserId = request.Order.UserId.ToString();

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
