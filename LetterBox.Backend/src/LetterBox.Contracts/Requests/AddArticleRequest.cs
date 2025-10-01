using LetterBox.Application.Articles.AddArticle;

namespace LetterBox.Contracts.Requests
{
    public record AddArticleRequest(
        string Title,
        string Content,
        string Slug,
        string Excerpt,
        string Status,
        string FeaturedImage,
        int ViewsCount,
        Guid CategoryId)
    {
        public AddArticleCommand ToCommand() =>
            new(Title, Content, Slug, Excerpt, Status, FeaturedImage, ViewsCount, CategoryId);
    }
}
