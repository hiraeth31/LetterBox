using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Models;
using LetterBox.Domain.Common;
using System.Security.Claims;

namespace LetterBox.Application.Authorization
{
    public interface ITokenProvider
    {
        Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default);
        Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
    }
}
