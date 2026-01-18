using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.CartItems.Queries.Handlers
{
    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartItemByIdQuery, CartItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartItemByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartItemDto?> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CartItemId is null)
            {
                throw new BadRequestException("Cart item id can't be null.");
            }

            var cartItem = await _unitOfWork.CartItemRepository.GetCartItemByIdAsync(request.CartItemId, request.CartId, request.ProductVariantId);

            if (cartItem is null)
            {
                throw new NotFoundException("Cart item not found for the given id.");
            }

            return cartItem;
        }
    }
}
