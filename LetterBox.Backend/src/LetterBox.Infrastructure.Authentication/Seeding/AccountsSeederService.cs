using LetterBox.Application.Accounts.DataModels;
using LetterBox.Infrastructure.Authentication.IdentityManagers;
using LetterBox.Infrastructure.Authentication.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LetterBox.Infrastructure.Authentication.Seeding
{
    public class AccountsSeederService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AccountsManager _adminAccountManager;
        private readonly PermissionManager _permissionManager;
        private readonly RolePermissionManager _rolePermissionManager;
        private readonly AdminOptions _adminOptions;

        public AccountsSeederService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            AccountsManager adminAccountManager,
            PermissionManager permissionManager,
            RolePermissionManager rolePermissionManager,
            IOptions<AdminOptions> adminOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _adminAccountManager = adminAccountManager;
            _permissionManager = permissionManager;
            _rolePermissionManager = rolePermissionManager;
            _adminOptions = adminOptions.Value;
        }
        public async Task SeedAsync()
        {
            var json = await File.ReadAllTextAsync("accounts.json");


            var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                ?? throw new ApplicationException("Deserialization error");

            await SeedPermissions(seedData);

            await SeedRoles(seedData);

            await SeedRolePermissions(seedData);

            var adminRole = await _roleManager.FindByNameAsync(AdminAccount.ADMIN)
                ?? throw new ApplicationException("Admin role doesn't exist");

            var adminUser = User.CreateAdmin(_adminOptions.UserName, _adminOptions.Email, adminRole);
            var result = await _userManager.CreateAsync(adminUser, _adminOptions.Password);

            var fullName = "Kirill Krotov";
            var adminAccount = new AdminAccount(fullName, adminUser);

            await _adminAccountManager.CreateAdminAccount(adminAccount);
        }

        private async Task SeedRolePermissions(RolePermissionOptions seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                var rolePermissions = seedData.Roles[roleName];

                await _rolePermissionManager.AddRangeIfExist(role!.Id, seedData.Roles[roleName]);
            }
        }

        private async Task SeedRoles(RolePermissionOptions seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    await _roleManager.CreateAsync(new Role { Name = roleName });
                }
            }
        }

        private async Task SeedPermissions(RolePermissionOptions seedData)
        {
            var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);

            await _permissionManager.AddRangeIfExist(permissionsToAdd);
        }
    }
}
