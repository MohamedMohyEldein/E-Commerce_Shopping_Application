using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.WishListItem.Commands;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Application.Features.WishListItem.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class WishListItemController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{WishListId:ulid}")]
        public async Task<ActionResult<List<WishListItemDto>>> GetAllWishListItems(Ulid? WishListId)
        {
            var result = await _mediator.Send(new GetAllWishListItemsQuery(WishListId));
            return Ok(result);
        }

        [HttpGet("{WishListItemId:ulid}/{WishListId:ulid}/{ProductVariantId}")]
        public async Task<ActionResult<WishListItemDto?>> GetWishListItemById(Ulid? WishListItemId, Ulid? WishListId, Ulid? ProductVariantId)
        {
            var result = await _mediator.Send(new GetWishListItemByIdQuery(WishListItemId, WishListId, ProductVariantId));
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("{WishListId:ulid}")]
        public async Task<ActionResult<WishListItemDto>> CreateWishListItem(Ulid? WishListId, [FromBody] CreateWishListItemDto wishListItemDto, IValidator<CreateWishListItemDto> validator)
        {
            wishListItemDto.WishlistId = WishListId.Value;
            try
            {
                validator.ValidateAndThrow(wishListItemDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateWishListItemCommand(wishListItemDto));
            return CreatedAtAction(nameof(GetWishListItemById), new { WishListItemId = result.Id, WishListId = result.WishListId, ProductVariantId = result.ProductVariantId }, result);
        }

        [HttpDelete("{WishListItemId:ulid}/{WishListId:ulid}/{ProductVariantId}")]
        public async Task<IActionResult> DeleteWishListItem(Ulid? WishListItemId, Ulid? WishListId, Ulid? ProductVariantId)
        {
            await _mediator.Send(new DeleteWishListItemCommand(WishListItemId, WishListId, ProductVariantId));
            return NoContent();
        }

    }
}
