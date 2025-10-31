using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
using LetterBox.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace LetterBox.Application.Accounts.LoginUser
{
    public class LoginUserHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvider;

        public LoginUserHandler(
            UserManager<User> userManager,
            ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<string, ErrorList>> Handle(LoginUserCommand command, CancellationToken cancellation = default)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                return Errors.General.NotFound().ToErrorList();

            var passwordConfirmation = await _userManager.CheckPasswordAsync(user, command.Password);
            if (!passwordConfirmation)
                return Errors.User.UserError().ToErrorList();

            var token = _tokenProvider.GenerateAccessToken(user);

            return token;
        }
    }
}
