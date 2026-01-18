using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.WishListItem.Commands.Handlers
{
    public class CreateWishListItemHandler : IRequestHandler<CreateWishListItemCommand, WishListItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWishListItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WishListItemDto?> Handle(CreateWishListItemCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateWishListItemDto is null)
            {
                throw new BadRequestException("Wish list item dto can't be null.");
            }

            var entity = await _unitOfWork.WishListItemRepository.GetWishListAndProductVarientByIdAsync(request.CreateWishListItemDto.WishlistId, request.CreateWishListItemDto.ProductVariantId);

            if (entity == false)
            {
                throw new NotFoundException($"Wish list with id {request.CreateWishListItemDto.WishlistId} or Product Varient with id {request.CreateWishListItemDto.WishlistId} not found.");
            }

            var wishListItem = _mapper.Map<WishlistItem>(request);
            wishListItem.Id = Ulid.NewUlid();

            await _unitOfWork.WishlistItems.AddAsync(wishListItem);
            await _unitOfWork.SaveChangesAsync();

            return await _unitOfWork.WishListItemRepository.GetWishListItemByIdAsync(wishListItem.Id, wishListItem.WishlistId, wishListItem.ProductVariantId);
        }
    }
}
