using HumanResourcesSystemRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HumanResourcesSystem
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role {  get; set; }
        public RoleRequirement(string role) {
            Role = role;
        }
    }
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly AppDbContext _appDbContext;
        public RoleAuthorizationHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Fail();
                return;
            }
            var roleName = requirement.Role.ToUpper();
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == roleName);
            var hasRole = await _appDbContext.UserRoles.AnyAsync(x => x.RoleId == role.Id && x.UserId == userId );
            if (hasRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
