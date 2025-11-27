using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Articles
{
    public interface IArticlesRepository
    {
        Task<Guid> Add(Article article, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Article>> GetTotalData(CancellationToken cancellationToken = default);

        Task<Result<Article, ErrorList>> GetById(Guid id, CancellationToken cancellationToken = default);
        
        Task<int> GetTotalCount(CancellationToken cancellationToken = default);
    }
}
