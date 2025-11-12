namespace LetterBox.Application.Models
{
    public record JwtTokenResult(string AccessToken, Guid Jti);
}
