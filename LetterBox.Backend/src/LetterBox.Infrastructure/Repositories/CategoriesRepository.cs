using LetterBox.Application.Categories;
using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Add(Category category, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Categories.AddAsync(category, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return category.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
