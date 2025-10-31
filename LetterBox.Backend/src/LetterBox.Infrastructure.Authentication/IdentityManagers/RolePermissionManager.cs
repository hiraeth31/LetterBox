using LetterBox.Application.Accounts.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LetterBox.Infrastructure.Authentication.IdentityManagers
{
    public class RolePermissionManager(AccountsDbContext accountsContext)
    {
        public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
        {
            foreach (var permissionCode in permissions)
            {
                var permission = await accountsContext.Permissions.FirstOrDefaultAsync(x => x.Code == permissionCode);
                if (permission is null)
                    throw new ApplicationException("permission is null");

                var rolePermissionExist = await accountsContext.RolePermissions
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id);

                if (rolePermissionExist)
                    continue;

                accountsContext.RolePermissions.Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission!.Id
                });
            }

            await accountsContext.SaveChangesAsync();
        }
    }
}
