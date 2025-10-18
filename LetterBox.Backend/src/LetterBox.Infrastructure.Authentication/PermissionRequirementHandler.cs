using Microsoft.AspNetCore.Authorization;

namespace LetterBox.Infrastructure.Authentication
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAttribute permission)
        {
            var userPermission = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");

            if (userPermission == null)
            {
                return;
            }

            if (userPermission.Value == permission.Code)
            {
                context.Succeed(permission);
            }
        }
    }
}
