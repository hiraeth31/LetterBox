using CSharpFunctionalExtensions;
using LetterBox.Application.Dtos;
using LetterBox.Domain.ArticlesManagement;

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

        public async Task<Result<Guid>> Handle(AddArticleCommand command, CancellationToken cancellationToken = default)
        {
            var articleId = Guid.NewGuid();

            // id category add
            var article = new Article(
                articleId,
                command.Title,
                command.Content,
                command.Slug,
                command.Excerpt,
                command.Status,
                command.FeaturedImage,
                command.ViewsCount,
                Guid.NewGuid());

            await _articlesRepository.Add(article);

            return article.Id;
        }
    }
}
