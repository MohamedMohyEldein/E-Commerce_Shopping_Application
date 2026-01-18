using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.WishListItem.Commands.Handlers
{
    public class DeleteWishListItemHandler : IRequestHandler<DeleteWishListItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishListItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWishListItemCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new BadRequestException("Request can't be null.");
            }

            var result = await _unitOfWork.WishListItemRepository.RemoveWishListItemAsync(request.WishListItemId, request.WishListId, request.ProductVariantId);

            if (result == false)
            {
                throw new NotFoundException($"Wish list item with id {request.WishListItemId} not found.");
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
