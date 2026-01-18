using AutoMapper;
using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Orders.Commands.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new BadRequestException("DeleteOrderCommand request cannot be null.");
            }
            var order = await _unitOfWork.OrderRepository.GetUserOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.Id} not found for user {request.UserId}.");
            }
            _unitOfWork.Orders.Remove(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
