using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Categories
{
    public interface ICategoriesRepository
    {
        Task<Guid> Add(Category category, CancellationToken cancellationToken = default);

        Task<int> GetTotalCount(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Category>> GetTotalData(CancellationToken cancellationToken = default);

        Task<Result<Category, ErrorList>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}
