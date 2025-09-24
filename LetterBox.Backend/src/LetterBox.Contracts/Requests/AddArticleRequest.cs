using LetterBox.Application.Dtos;

namespace LetterBox.Contracts.Requests
{
    public record AddArticleRequest(
        string Title,
        string Content,
        string Slug,
        string Excerpt,
        string Status,
        string FeaturedImage,
        int ViewsCount)
    {
        public AddArticleCommand ToCommand() =>
            new(Title, Content, Slug, Excerpt, Status, FeaturedImage, ViewsCount);
    }
}
