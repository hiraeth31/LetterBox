using CSharpFunctionalExtensions;
using LetterBox.Application.Accounts.DataModels;
using LetterBox.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LetterBox.Application.Accounts.RegisterUser
{
    public class RegisterUserHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RegisterUserHandler(UserManager<User> userManager, 
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<UnitResult<ErrorList>> Handle(
            RegisterUserCommand command,
            CancellationToken cancellationToken = default)
        {
            var existedUser = await _userManager.FindByEmailAsync(command.Email);
            if (existedUser != null)
            {
                return Errors.User.UserError().ToErrorList();
            }

            var baseRole = await _roleManager.FindByNameAsync("BASE")
                ?? throw new ApplicationException("Base role doesn't exist");

            //var user = new User
            //{
            //    UserName = command.UserName,
            //    Email = command.Email,
            //};

            var user = User.CreateUser(command.UserName, command.Email, baseRole);

            var userResult = await _userManager.CreateAsync(user, command.Password);
            if (userResult.Succeeded)
            {
                return Result.Success<ErrorList>();
            }

            var errors = userResult.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

            return new ErrorList(Errors.General.ValueIsInvalid("123").ToErrorList());
        }
    }
}
