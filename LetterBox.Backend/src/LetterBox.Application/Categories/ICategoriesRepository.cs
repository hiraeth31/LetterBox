using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Categories
{
    public interface ICategoriesRepository
    {
        Task<Guid> Add(Category category, CancellationToken cancellationToken = default);

        Task<int> GetTotalCount(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Category>> GetTotalData(CancellationToken cancellationToken = default);
    }
}
