using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Application.Categories
{
    public interface ICategoriesRepository
    {
        Task<Guid> Add(Category category, CancellationToken cancellationToken = default);
    }
}
