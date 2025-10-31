using LetterBox.Application.Accounts.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LetterBox.Infrastructure.Authentication.IdentityManagers
{
    public class PermissionManager(AccountsDbContext accountsContext)
    {
        public async Task<Permission?> FindByCode(string code) =>
            await accountsContext.Permissions.FirstOrDefaultAsync(x => x.Code == code);

        public async Task AddRangeIfExist(IEnumerable<string> permissions)
        {
            foreach (var permissionCode in permissions)
            {
                var isPermissionExist = await accountsContext.Permissions
                    .AnyAsync(p => p.Code == permissionCode);

                if (isPermissionExist)
                    continue;

                await accountsContext.Permissions.AddAsync(new Permission { Code = permissionCode });
            }

            await accountsContext.SaveChangesAsync();
        }

        public async Task<HashSet<string>> GetUserPermissionCodes(Guid userId)
        {
            var permissions = await accountsContext.Users
                .Include(r => r.Roles)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission.Code)
                .ToListAsync();

            return permissions.ToHashSet();
        }
    }
}
