using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Articles.GetArticle
{
    public class GetAllArticlesHandler
    {
        private readonly IArticlesRepository articlesRepository;

        public GetAllArticlesHandler(IArticlesRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        /// <summary>
        /// Метод get для получения всех статей (без фильтров)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>возврат - IReadonlyList<Article> (массив всех статей) </returns>
        public async Task<IReadOnlyList<Article>> Handle(CancellationToken cancellationToken = default)
        {
            return await articlesRepository.GetAll(cancellationToken);
        }
    }
}
