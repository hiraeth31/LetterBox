using CSharpFunctionalExtensions;
using LetterBox.Application.Articles;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Categories.AddCategory
{
    public class AddCategoryHandler
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public AddCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public async Task<Result<Guid>> Handle(AddCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var categoryId = Guid.NewGuid();

            var category = new Category(
                categoryId,
                command.Name,
                command.Slug,
                command.Excerpt,
                command.Description);

            await _categoriesRepository.Add(category, cancellationToken);

            return category.Id;
        }
    }
}
