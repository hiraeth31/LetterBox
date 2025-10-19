using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.AddArticle
{
    public class AddArticleHandler
    {
        private readonly IArticlesRepository _articlesRepository;

        public AddArticleHandler(
            IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddArticleCommand command, CancellationToken cancellationToken = default)
        {
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
