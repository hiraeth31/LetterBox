using LetterBox.Application.Articles;
using LetterBox.Domain.ArticlesManagement;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IReadOnlyList<Article>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.Articles.ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
