using AutoMapper;
using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.OrderItems.Commands.Handlers
{
    public class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItemcommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderItemcommand request, CancellationToken cancellationToken)
        {
            if (request.orderItemId is null || request.orderId is null || request.productVariantId is null) {
                throw new BadRequestException("DeleteOrderItemCommand request cannot be null.");
            }

            bool result = await _unitOfWork.OrderItemRepository.RemoveOrderItemAsync(request.orderItemId, request.orderId, request.productVariantId);

            if (!result)
            {
                throw new NotFoundException($"OrderItem not found.");
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
