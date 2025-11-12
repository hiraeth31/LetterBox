using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace LetterBox.Infrastructure.Authentication.IdentityManagers
{
    public class AccountsManager(AccountsDbContext accountsContext)
    {
        public async Task CreateAdminAccount(AdminAccount adminAccount)
        {
            await accountsContext.AdminAccounts.AddAsync(adminAccount);
            await accountsContext.SaveChangesAsync();
        }
    }
}
