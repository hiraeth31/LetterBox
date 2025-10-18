using LetterBox.Application.Accounts.DataModels;

namespace LetterBox.Application.Authorization
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(User user);
    }
}
