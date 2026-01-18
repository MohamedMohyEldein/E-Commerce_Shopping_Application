using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.CartItems.Commands.Handlers
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new BadRequestException("Request can't be null.");
            }

            var result = await _unitOfWork.CartItemRepository.RemoveCartItemAsync(request.CartItemId, request.CartId, request.ProductVariantId);

            if (result == false)
            {
                throw new NotFoundException($"Cart item with id {request.CartItemId} not found.");
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
