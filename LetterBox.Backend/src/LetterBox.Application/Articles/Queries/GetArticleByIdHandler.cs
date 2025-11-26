using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.Queries
{
    public class GetArticleByIdHandler
    {
        private readonly IArticlesRepository articlesRepository;

        public GetArticleByIdHandler(IArticlesRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        /// <summary>
        /// Метод хендлер для вытягивания категории по guid
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Article, ErrorList>> Handle(Guid guid, CancellationToken cancellationToken = default)
        {
            var articleResult = await articlesRepository.GetById(guid, cancellationToken);

            if (articleResult.IsFailure)
            {
                return Errors.General.Failure().ToErrorList();
            }

            return articleResult.Value;
        }
    }
}
