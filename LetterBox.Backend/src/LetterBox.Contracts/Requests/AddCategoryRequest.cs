using LetterBox.Application.Categories.AddCategory;

namespace LetterBox.Contracts.Requests
{
    public record AddCategoryRequest(
        string Name,
        string Slug,
        string Excerpt,
        string Description)
    {
        public AddCategoryCommand ToCommand() =>
            new(Name, Slug, Excerpt, Description);
    }
}
