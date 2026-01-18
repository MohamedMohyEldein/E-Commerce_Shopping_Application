using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Authentication.Commands;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Presentation.Controllers
{
    public class AuthController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthResultDto>> PostRegister([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(PostLogin), new { }, result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> PostLogin([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
