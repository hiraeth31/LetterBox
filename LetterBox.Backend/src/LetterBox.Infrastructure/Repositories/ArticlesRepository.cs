using CSharpFunctionalExtensions;
using LetterBox.Application.Articles;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;
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

        /// <summary>
        /// Описание метода getbyid (article) - linq запрос .firstOrDefaultAsync()
        /// </summary>
        /// <param name="a_id">guid Article (статьи)</param>
        /// <param name="cts"></param>
        /// <returns></returns>
        public async Task<Result<Article, ErrorList>> GetById(
            Guid a_id, CancellationToken cts = default)
        {
            try
            {
                var article = await _dbContext.Articles.
                    FirstOrDefaultAsync(a => a.Id == a_id, cts);

                if(article is null) 
                    return Errors.General.NotFound(a_id).ToErrorList();

                return article;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
