using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Articles
{
    public interface IArticlesRepository
    {
        Task<Guid> Add(Article article, CancellationToken cancellationToken = default);
        
        Task<int> GetTotalCount(CancellationToken cancellationToken = default);
    }
}
