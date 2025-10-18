using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using Microsoft.AspNetCore.Identity;

namespace LetterBox.Application.Accounts.RegisterUser
{
    public class RegisterUserHandler
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<string>> Handle(
            RegisterUserCommand command,
            CancellationToken cancellationToken = default)
        {
            //var existedUser = await _userManager.FindByEmailAsync(command.Email);
            //if (existedUser != null)
            //{
            //    return "something went wrong";
            //}

            var user = new User
            {
                UserName = command.UserName,
                Email = command.Email,
            };

            var userResult = await _userManager.CreateAsync(user, command.Password);
            if (!userResult.Succeeded)
                return "something went wrong";



            return "user created";
        }
    }
}
