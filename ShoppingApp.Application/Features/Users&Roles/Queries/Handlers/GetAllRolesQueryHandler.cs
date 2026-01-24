using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Queries.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<string>?>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>?> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles =  await _roleManager.Roles.ToListAsync();
            return roles.Select(r => r.Name!).ToList();
        }
    }
}
