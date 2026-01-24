using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Users_Roles.Commands;
using ShoppingApp.Application.Features.Users_Roles.Commands.Handlers;
using ShoppingApp.Application.Features.Users_Roles.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class RolesController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string? email)
        {
            var result = await _mediator.Send(new GetUserRolesQuery(email));
            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> CreateRole(string? roleName)
        {
            var result = await _mediator.Send(new CreateRoleCommand(roleName));

            if (!result) return BadRequest("Role could not be created.");

            return Created("Role created successfully.", result);
        }

        [HttpPost("AssignUserToRole")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AssignUserToRole(string? email, string? roleName)
        {
            var result = await _mediator.Send(new AssignUserToRoleCommand(email, roleName));

            if (!result) return BadRequest("User could not be assigned to role.");

            return Ok("User assigned to role successfully.");
        }

        [HttpPost("RemoveUserFromRole")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> RemoveUserFromRole(string? email, string? roleName)
        {
            var result = await _mediator.Send(new RemoveUserFromRoleCommand(email, roleName));

            if (!result) return BadRequest("User could not be removed from role.");

            return Ok("User removed from role successfully.");
        }

        [HttpPost("AddUserClaim")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddUserClaim(string? userEmail, string? claimType, string? claimValue)
        {
            var result = await _mediator.Send(new AddUserClaimCommand(userEmail, claimType, claimValue));
            if (!result) return BadRequest("Claim could not be added to user.");
            return Ok("Claim added to user successfully.");
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteUser(string? email)
        {
            var result = await _mediator.Send(new DeleteUserCommand(email));
            if (!result) return BadRequest("User could not be deleted.");
            return Ok("User deleted successfully.");
        }
    }
}
