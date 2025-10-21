using CSharpFunctionalExtensions;
using FluentValidation;
using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Articles.AddArticle
{
    public class AddArticleHandler
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly IValidator<AddArticleCommand> _validator;

        public AddArticleHandler(
            IArticlesRepository articlesRepository, 
            IValidator<AddArticleCommand> validator)
        {
            _articlesRepository = articlesRepository;

            _validator = validator;

        }

        public async Task<Result<Guid>> Handle(AddArticleCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
            {

            }

            var articleId = Guid.NewGuid();

            var article = new Article(
                articleId,
                command.Title,
                command.Content,
                command.Slug,
                command.Excerpt,
                command.Status,
                command.FeaturedImage,
                command.ViewsCount,
                command.CategoryId);

            await _articlesRepository.Add(article, cancellationToken);

            return article.Id;
        }
    }
}
