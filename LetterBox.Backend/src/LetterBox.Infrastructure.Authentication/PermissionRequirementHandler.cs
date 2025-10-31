using LetterBox.Infrastructure.Authentication.IdentityManagers;
using LetterBox.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LetterBox.Infrastructure.Authentication
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAttribute permission)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManager>();

            var userIdString = context.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaims.Id)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
            {
                context.Fail();
                return;
            }

            var permissions = await permissionManager.GetUserPermissionCodes(userId);

            if (permissions.Contains(permission.Code))
            {
                context.Succeed(permission);
                return;
            }

            context.Fail();
        }
    }
}
