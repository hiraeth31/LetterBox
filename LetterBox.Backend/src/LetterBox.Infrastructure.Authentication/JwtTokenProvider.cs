using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
using LetterBox.Application.Models;
using LetterBox.Domain.Common;
using LetterBox.Infrastructure.Authentication.Factories;
using LetterBox.Infrastructure.Authentication.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LetterBox.Infrastructure.Authentication
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly AccountsDbContext _accountContext;

        public JwtTokenProvider(
            IOptions<JwtOptions> options,
            AccountsDbContext accountContext)
        {
            _jwtOptions = options.Value;
            _accountContext = accountContext;
        }

        public async Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

            var jti = Guid.NewGuid();

            Claim[] claims =
                [
                    new Claim(CustomClaims.Id, user.Id.ToString()),
                    new Claim(CustomClaims.Jti, jti.ToString()),
                    new Claim(CustomClaims.Email, user.Email ?? "")
                ];

            claims = claims.Concat(roleClaims).ToArray();

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutesTime)),
                signingCredentials: signingCredentials,
                claims: claims);

            var jwtStringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtTokenResult(jwtStringToken, jti);
        }

        public async Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken)
        {
            var refreshSession = new RefreshSession
            {
                User = user,
                CreatedAt = DateTime.UtcNow,
                ExpiresIn = DateTime.UtcNow.AddDays(30),
                Jti = accessTokenJti,
                RefreshToken = Guid.NewGuid(),
            };

            _accountContext.Add(refreshSession);
            await _accountContext.SaveChangesAsync(cancellationToken);

            return refreshSession.RefreshToken;
        }

        public async Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var validationParameters = TokenValidationParametersFactory.CreateWithoutLifeTime(_jwtOptions);

            var validationResult = await jwtHandler.ValidateTokenAsync(jwtToken, validationParameters);
            if (validationResult.IsValid == false)
                return Errors.Tokens.InvalidToken();

            return validationResult.ClaimsIdentity.Claims.ToList();
        }
    }
}
