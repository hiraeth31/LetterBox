namespace LetterBox.Application.Dtos
{
    public record AddArticleCommand(
        string Title,
        string Content,
        string Slug,
        string Excerpt,
        string Status,
        string FeaturedImage,
        int ViewsCount);
}
