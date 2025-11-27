using CSharpFunctionalExtensions;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles.GetArticle
{
    public class GetArticlesCountHandler
    {
        private readonly IArticlesRepository articlesRepository;

        public GetArticlesCountHandler(IArticlesRepository articlesRepository)
        {
             this.articlesRepository = articlesRepository;
        }

        /// <summary>
        /// получение количества всех статей из DB
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> возврат - int количество статей (принять в EndpointRerusult<int>)</returns>
        public async Task<Result<int, ErrorList>> Handle(CancellationToken cancellationToken = default)
        {
            return await articlesRepository.GetTotalCount(cancellationToken);
        }
    }
}
