using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Accounts.Responses;
using LetterBox.Application.Authorization;
using LetterBox.Application.Models;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Accounts.RefreshTokens
{
    public class RefreshTokensHandler
    {
        private readonly IRefreshSessionManager _refreshSessionManager;
        private readonly ITokenProvider _tokenProvider;

        public RefreshTokensHandler(
            IRefreshSessionManager refreshSessionManager,
            ITokenProvider tokenProvider)
        {
            _refreshSessionManager = refreshSessionManager;
            _tokenProvider = tokenProvider;
        }
        public async Task<Result<LoginResponse, ErrorList>> Handle
            (RefreshTokensCommand command,
            CancellationToken cancellationToken = default)
        {
            var oldRefreshSession = await _refreshSessionManager.GetByRefreshToken(command.RefreshToken, cancellationToken);
            if (oldRefreshSession.IsFailure)
                return oldRefreshSession.Error.ToErrorList();

            if (oldRefreshSession.Value.ExpiresIn < DateTime.UtcNow)
                return Errors.Tokens.ExpiredToken().ToErrorList();

            //var userClaims = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
            //if (userClaims.IsFailure)
            //    return Errors.Tokens.InvalidToken().ToErrorList();

            //var userIdString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
            //if (!Guid.TryParse(userIdString, out var userId))
            //    return Errors.General.Failure().ToErrorList();

            //if (oldRefreshSession.Value.UserId != userId)
            //    return Errors.Tokens.InvalidToken().ToErrorList();

            //var userJtiString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
            //if (!Guid.TryParse(userJtiString, out var userJtiGuid))
            //    return Errors.General.Failure().ToErrorList();

            //if (oldRefreshSession.Value.Jti != userJtiGuid)
            //    return Errors.Tokens.InvalidToken().ToErrorList();

            _refreshSessionManager.Delete(oldRefreshSession.Value);

            var accessToken = await _tokenProvider.GenerateAccessToken(oldRefreshSession.Value.User, cancellationToken);
            var refreshToken = await _tokenProvider.GenerateRefreshToken(oldRefreshSession.Value.User, accessToken.Jti, cancellationToken);

            return new LoginResponse(
                accessToken.AccessToken,
                refreshToken,
                oldRefreshSession.Value.User.Id,
                oldRefreshSession.Value.User.Email!);
        }
    }
}
