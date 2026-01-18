using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Orders.Commands.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new BadRequestException("UpdateOrderCommand request can't be null.");
            }

            var order = await _unitOfWork.OrderRepository.GetUserOrderByIdAsync(request.Id, request.Order.UserId);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.Id} not found for user {request.Order.UserId}.");
            }


            order.OrderDate = request.Order.OrderDate;
            order.Status = request.Order.Status;

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
