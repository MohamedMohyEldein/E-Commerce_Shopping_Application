using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.OrderItems.Commands;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Application.Features.OrderItems.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    [Route("api/Orders/{orderId:ulid}/Items")]
    public class OrderItemsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{orderItemId:ulid}/{productVariantId:ulid}")]
        public async Task<ActionResult<OrderItemDto?>> GetById(Ulid orderId, Ulid orderItemId, Ulid productVariantId)
        {
            var result = await _mediator.Send(new GetOrderItemByIdQuery(orderId, orderItemId, productVariantId));
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderItemDto>>> GetAll(Ulid orderId)
        {
            var result = await _mediator.Send(new GetAllOrderItemsQuery(orderId));
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> CreateOrderItem(Ulid orderId, [FromBody] CreateOrderItemDto? orderItem, IValidator<CreateOrderItemDto> validator)
        {
            orderItem!.OrderId = orderId;
            try
            {
                validator.ValidateAndThrow(orderItem);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateOrderItemcommand(orderItem));
            return CreatedAtAction(nameof(GetById), new { orderId = orderItem.OrderId, productVariantId = orderItem.ProductVariantId }, result);
        }
        //[HttpPut("{orderItemId:ulid}")]
        //public async Task<ActionResult<OrderItemDto>> Update(Ulid orderId, Ulid orderItemId, [FromBody] UpdateOrderItemDto orderItem, IValidator<UpdateOrderItemDto> validator)
        //{
        //    orderItem.OrderId = orderId;
        //    try
        //    {
        //        validator.ValidateAndThrow(orderItem);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return BadRequest(ex.Errors);
        //    }
        //    var result = await _mediator.Send(new UpdateOrderItemcommand(orderItemId, orderItem));
        //    return Ok(result);
        //}
        [HttpDelete("{OrderItemId:ulid}/{ProductVariantId:ulid}")]
        public async Task<ActionResult> DeleteOrderItem(Ulid orderId, Ulid OrderItemId, Ulid ProductVariantId)
        {
            var result = await _mediator.Send(new DeleteOrderItemcommand(OrderItemId, orderId, ProductVariantId));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
