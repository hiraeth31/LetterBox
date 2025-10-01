using LetterBox.Application.Articles;
using LetterBox.Domain.ArticlesManagement;

namespace LetterBox.Infrastructure.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ArticlesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(
            Article article, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Articles.AddAsync(article, cancellationToken);
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                return article.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
