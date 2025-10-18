using LetterBox.Application.Accounts.RegisterUser;

namespace LetterBox.Contracts.Requests
{
    public record RegisterUserRequest(string Email, string UserName, string Password)
    {
        public RegisterUserCommand ToCommand() =>
            new(Email, UserName, Password);
    }
}
