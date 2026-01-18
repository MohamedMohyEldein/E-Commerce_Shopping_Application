using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.CartItems.Commands.Handlers
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, CartItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartItemDto?> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            if(request.CreateCartItemDto is null)
            {
                throw new BadRequestException("Cart item dto can't be null.");
            }

            var entity = await _unitOfWork.CartItemRepository.GetCartAndProductVarientByIdAsync(request.CreateCartItemDto.CartId, request.CreateCartItemDto.ProductVariantId);

            if (entity == false)
            {
                throw new NotFoundException($"Cart with id {request.CreateCartItemDto.CartId} or Product Varient with id {request.CreateCartItemDto.CartId} not found.");
            }

            var cartItem = _mapper.Map<CartItem>(request);
            cartItem.Id = Ulid.NewUlid();

            await _unitOfWork.CartItems.AddAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();

            return await _unitOfWork.CartItemRepository.GetCartItemByIdAsync(cartItem.Id, cartItem.CartId, cartItem.ProductVariantId);
        }
    }
}
