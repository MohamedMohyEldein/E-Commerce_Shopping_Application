using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.CartItems.Commands;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Application.Features.CartItems.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class CartItemController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{CartId:ulid}")]
        public async Task<ActionResult<List<CartItemDto>>> GetAllCartItems(Ulid? CartId)
        {
            var result = await _mediator.Send(new GetAllCartItemsQuery(CartId));
            return Ok(result);
        }

        [HttpGet("{CartItemId:ulid}/{CartId:ulid}/{ProductVariantId}")]
        public async Task<ActionResult<CartItemDto?>> GetCartItemById(Ulid? CartItemId, Ulid? CartId, Ulid? ProductVariantId)
        {
            var result = await _mediator.Send(new GetCartItemByIdQuery(CartItemId, CartId, ProductVariantId));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost("{CartId:ulid}")]
        public async Task<ActionResult> CreateCartItem(Ulid? CartId, [FromBody] CreateCartItemDto cartItemDto, IValidator<CreateCartItemDto> validator)
        {
            cartItemDto.CartId = CartId.Value;
            
            try
            {
                validator.ValidateAndThrow(cartItemDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }

            var result = await _mediator.Send(new CreateCartItemCommand(cartItemDto));
            return CreatedAtAction(nameof(GetCartItemById), new { CartItemId = result.Id, CartId = result.CartId, ProductVariantId = result.ProductVariantId }, result);
        }

        [HttpDelete("{CartItemId:ulid}/{CartId:ulid}/{ProductVariantId}")]
        public async Task<IActionResult> DeleteCartItem(Ulid? CartItemId, Ulid? CartId, Ulid? ProductVariantId)
        {
            await _mediator.Send(new DeleteCartItemCommand(CartItemId, CartId, ProductVariantId));
            return NoContent();
        }
    }
}
