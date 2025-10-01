namespace LetterBox.Application.Categories.AddCategory
{
    public record AddCategoryCommand(
        string Name,
        string Slug,
        string Excerpt,
        string Description);
}
