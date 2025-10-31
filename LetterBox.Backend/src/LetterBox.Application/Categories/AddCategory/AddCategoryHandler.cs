using CSharpFunctionalExtensions;
using FluentValidation;
using LetterBox.Application.Articles;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Extensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Categories.AddCategory
{
    public class AddCategoryHandler
    {
        private readonly ICategoriesRepository _categoriesRepository;

        private readonly IValidator<AddCategoryCommand> _validator;

        public AddCategoryHandler(ICategoriesRepository categoriesRepository, IValidator<AddCategoryCommand> validator)
        {
            _categoriesRepository = categoriesRepository;

            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(AddCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

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
