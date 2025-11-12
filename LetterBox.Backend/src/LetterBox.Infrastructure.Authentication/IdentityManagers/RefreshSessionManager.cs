using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
using LetterBox.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LetterBox.Infrastructure.Authentication.IdentityManagers
{
    public class RefreshSessionManager(AccountsDbContext accountsContext) : IRefreshSessionManager
    {
        public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
            Guid refreshToken,
            CancellationToken cancellationToken)
        {
            var refreshSession = await accountsContext.RefreshSessions
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, cancellationToken);

            if (refreshSession is null)
                return Errors.General.NotFound();

            return refreshSession;
        }

        public void Delete(RefreshSession refreshSession)
        {
            accountsContext.RefreshSessions.Remove(refreshSession);
            accountsContext.SaveChanges();
        }
    }
}
