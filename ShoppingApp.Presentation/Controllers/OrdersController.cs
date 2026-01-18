using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Orders.Commands;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Application.Features.Orders.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class OrdersController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{UserId:ulid}/{OrderId:ulid}")]
        public async Task<ActionResult<OrderDto?>> GetById(Ulid UserId, Ulid OrderId)
        {
            var result = await _mediator.Send(new GetUserOrderByIdQuery(UserId, OrderId));
            return Ok(result);
        }
        [HttpGet("{UserId:ulid}")]
        public async Task<ActionResult<List<OrderDto>>> GetAllAsync(Ulid UserId)
        {
            var result = await _mediator.Send(new GetAllUserOrdersQuery(UserId));
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto? Order, IValidator<CreateOrderDto> validator)
        {
            try
            {
                validator.ValidateAndThrow(Order);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateOrderCommand(Order));
            return CreatedAtAction(nameof(GetById), new { UserId = result.UserId, OrderId = result.Id }, result);
        }
        [HttpPut("{OrderId:ulid}")]
        public async Task<ActionResult<OrderDto>> Update(Ulid OrderId, [FromBody] UpdateOrderDto Order, IValidator<UpdateOrderDto> validator)
        {
            try
            {
                validator.ValidateAndThrow(Order);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new UpdateOrderCommand(OrderId, Order));
            return Ok(result);
        }
        [HttpDelete("{UserId:ulid}/{OrderId:ulid}")]
        public async Task<ActionResult> Delete(Ulid UserId, Ulid OrderId)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(OrderId, UserId));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
