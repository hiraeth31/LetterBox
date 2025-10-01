namespace LetterBox.Application.Articles.AddArticle
{
    public record AddArticleCommand(
        string Title,
        string Content,
        string Slug,
        string Excerpt,
        string Status,
        string FeaturedImage,
        int ViewsCount,
        Guid CategoryId);
}
