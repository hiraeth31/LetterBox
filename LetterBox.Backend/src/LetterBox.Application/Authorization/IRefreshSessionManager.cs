using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Authorization
{
    public interface IRefreshSessionManager
    {
        Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
        void Delete(RefreshSession refreshSession);
    }
}
