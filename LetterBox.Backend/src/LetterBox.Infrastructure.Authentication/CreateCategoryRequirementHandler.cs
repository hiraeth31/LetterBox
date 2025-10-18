using Microsoft.AspNetCore.Authorization;

namespace LetterBox.Infrastructure.Authentication
{
    public class CreateCategoryRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAttribute requirement)
        {
            throw new NotImplementedException();
        }
    }
}
