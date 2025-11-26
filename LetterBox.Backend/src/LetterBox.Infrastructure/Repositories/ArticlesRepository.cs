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

        public async Task<Guid> 
        Add(Article article, CancellationToken cancellationToken = default)
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
        /// <summary>
        /// Описание метода интерфейса для подстчёта всех статей (без фильтров)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Возврат - <int> количество статей в бд (общее количество) </returns>
        public async Task<int>
        GetTotalCount(CancellationToken cancellationToken = default)
        {
            try
            {
                int count = await _dbContext.Articles.CountAsync();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
