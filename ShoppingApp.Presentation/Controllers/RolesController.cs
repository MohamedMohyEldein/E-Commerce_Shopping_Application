using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Users_Roles.Commands;
using ShoppingApp.Application.Features.Users_Roles.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class RolesController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string? email)
        {
            var result = await _mediator.Send(new GetUserRolesQuery(email));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string? roleName)
        {
            var result = await _mediator.Send(new CreateRoleCommand(roleName));

            if (!result) return BadRequest("Role could not be created.");

            return Created("Role created successfully.", result);
        }

        [HttpPost("AssignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(string? email, string? roleName)
        {
            var result = await _mediator.Send(new AssignUserToRoleCommand(email, roleName));

            if (!result) return BadRequest("User could not be assigned to role.");

            return Ok("User assigned to role successfully.");
        }

        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string? email, string? roleName)
        {
            var result = await _mediator.Send(new RemoveUserFromRoleCommand(email, roleName));

            if (!result) return BadRequest("User could not be removed from role.");

            return Ok("User removed from role successfully.");
        }
    }
}
