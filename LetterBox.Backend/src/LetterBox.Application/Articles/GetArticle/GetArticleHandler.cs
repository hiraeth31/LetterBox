using CSharpFunctionalExtensions;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.GetArticle
{
    public class GetArticleHandler
    {
        private readonly IArticlesRepository _articlesRepository;

        public GetArticleHandler(IArticlesRepository _articlesRepository)
        {
            this._articlesRepository = _articlesRepository;
        }

        /// <summary>
        /// получение количества всех статей из DB
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> возврат - int количество статей (принять в EndpointResult<int>)</returns>
        public async Task<Result<int, ErrorList>> HandleTotalCount(CancellationToken cancellationToken = default)
        {
            return await _articlesRepository.GetTotalCount(cancellationToken);
        }
    }
}
