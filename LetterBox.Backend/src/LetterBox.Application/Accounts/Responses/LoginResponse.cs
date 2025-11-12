namespace LetterBox.Application.Accounts.Responses
{
    public record LoginResponse(string AccessToken, Guid RefreshToken, Guid UserId, string Email);
}
