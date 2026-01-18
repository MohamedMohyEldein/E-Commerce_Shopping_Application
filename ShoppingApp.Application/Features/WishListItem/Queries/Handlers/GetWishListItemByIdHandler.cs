using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.WishListItem.Queries.Handlers
{
    public class GetWishListItemByIdHandler : IRequestHandler<GetWishListItemByIdQuery, WishListItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishListItemByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WishListItemDto?> Handle(GetWishListItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.WishListItemId is null)
            {
                throw new BadRequestException("Wish list item id can't be null.");
            }

            var wishListItem = await _unitOfWork.WishListItemRepository.GetWishListItemByIdAsync(request.WishListItemId, request.WishListId, request.ProductVariantId);

            if (wishListItem is null)
            {
                throw new NotFoundException("Wish list item not found for the given id.");
            }

            return wishListItem;
        }
    }
}
