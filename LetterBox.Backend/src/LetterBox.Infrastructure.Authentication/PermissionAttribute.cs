using Microsoft.AspNetCore.Authorization;

namespace LetterBox.Infrastructure.Authentication
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Code { get; set; }

        public PermissionAttribute(string code) : base(policy: code)
        {
            Code = code;
        }
    }
}
