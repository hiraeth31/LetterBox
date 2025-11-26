namespace LetterBox.Application.Accounts.RegisterUser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    public record RegisterUserCommand(string Email, string UserName, string Password);
}
