﻿using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
using LetterBox.Infrastructure.Authentication.Models;
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

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

            Claim[] claims =
                [
                    new Claim(CustomClaims.Id, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
                ];

            claims = claims.Concat(roleClaims).ToArray();

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutesTime)),
                signingCredentials: signingCredentials,
                claims: claims);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return stringToken;
        }
    }
}
