using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Articles
{
    public interface IArticlesRepository
    {
        Task<Guid> Add(Article article, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Article>> GetAll(CancellationToken cancellationToken = default);
    }
}
