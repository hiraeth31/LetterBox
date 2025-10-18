using LetterBox.Application.Accounts.LoginUser;

namespace LetterBox.Contracts.Requests
{
    public record LoginUserRequest(string Email, string Password)
    {
        public LoginUserCommand ToCommand() =>
            new(Email, Password);
    }
}
