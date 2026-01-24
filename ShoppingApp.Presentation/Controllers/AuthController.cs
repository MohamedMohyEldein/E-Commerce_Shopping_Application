using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Authentication.Commands;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthResultDto>> PostRegister([FromBody] RegisterDto? registerDto)
        {
            var result = await _mediator.Send(new RegisterCommand(registerDto));

            return CreatedAtAction(nameof(PostLogin), new { }, result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> PostLogin([FromBody] LoginDto? loginDto)
        {
            var result = await _mediator.Send(new LoginCommand(loginDto));
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenResultDto>> PostRefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var result = await _mediator.Send(new RefreshTokenCommand(refreshTokenDto.Token, refreshTokenDto.UserEmail));
            return Ok(result);
        }
    }
}
