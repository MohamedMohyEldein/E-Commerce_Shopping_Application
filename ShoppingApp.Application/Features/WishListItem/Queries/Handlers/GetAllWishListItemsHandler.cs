using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.WishListItem.Queries.Handlers
{
    public class GetAllWishListItemsHandler : IRequestHandler<GetAllWishListItemsQuery, List<WishListItemDto?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWishListItemsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<WishListItemDto?>> Handle(GetAllWishListItemsQuery request, CancellationToken cancellationToken)
        {
            if(request.WishListId is null)
            {
                throw new BadRequestException("Wish list id can't be null.");
            }
            var wishListItems = await _unitOfWork.WishListItemRepository.GetAllWishListItemsAsync(request.WishListId);
            if(wishListItems is null)
            {
                throw new NotFoundException("No wish list items found for the given wish list id.");
            }
            return wishListItems;
        }
    }
}
