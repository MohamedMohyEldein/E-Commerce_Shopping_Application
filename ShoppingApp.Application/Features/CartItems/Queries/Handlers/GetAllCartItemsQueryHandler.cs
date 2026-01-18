using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.CartItems.Queries.Handlers
{
    public class GetAllCartItemsQueryHandler : IRequestHandler<GetAllCartItemsQuery, List<CartItemDto?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCartItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CartItemDto?>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
        {
            if (request.CartId is null)
            {
                throw new BadRequestException("Cart id can't be null.");
            }
            var cartItems = await _unitOfWork.CartItemRepository.GetAllCartItemsAsync(request.CartId);
            if (cartItems is null)
            {
                throw new NotFoundException("No cart items found for the given cart id.");
            }
            return cartItems;
        }
    }
}
