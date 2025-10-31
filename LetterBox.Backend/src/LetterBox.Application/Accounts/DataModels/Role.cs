using Microsoft.AspNetCore.Identity;

namespace LetterBox.Application.Accounts.DataModels
{
    public class Role : IdentityRole<Guid>
    {
        public List<RolePermission> RolePermissions { get; set; } = [];
    }
}
