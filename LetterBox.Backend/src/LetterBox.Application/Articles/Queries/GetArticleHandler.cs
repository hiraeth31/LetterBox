using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.Queries
{
    public class GetArticleHandler
    {
        private readonly IArticlesRepository _articlesRepository;

        public GetArticleHandler(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public async Task<Result<IReadOnlyList<Article>, ErrorList>> Handle(CancellationToken cancellationToken = default)
        {
            var articles = await _articlesRepository.GetAll(cancellationToken);
            return Result.Success<IReadOnlyList<Article>, ErrorList>(articles);
        }
    }
}
