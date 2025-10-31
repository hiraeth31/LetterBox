using LetterBox.Application.Accounts.DataModels;

namespace LetterBox.Infrastructure.Authentication.IdentityManagers
{
    public class AdminAccountManager(AccountsDbContext accountsContext)
    {
        public async Task CreateAdminAccount(AdminAccount adminAccount)
        {
            await accountsContext.AdminAccounts.AddAsync(adminAccount);
            await accountsContext.SaveChangesAsync();
        }
    }
}
