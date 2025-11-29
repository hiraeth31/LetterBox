using CSharpFunctionalExtensions;
using LetterBox.Application.Categories;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LetterBox.Infrastructure.Repositories
{
    /// <summary>
    /// класс для реализации linq запросов к БД для категорий
    /// </summary>
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Добавление категории (request)
        /// </summary>
        /// <param name="category"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// метод linq запрос для получения категории по ID (guid)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>FirstOrDefaultAsync(c => c.Id == c_id) --> Result<Category, ErrorList> </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result<Category, ErrorList>> GetById(Guid c_id, CancellationToken cts = default)
        { 
            try
            {
                var category = await _dbContext.Categories.
                    FirstOrDefaultAsync(c => c.Id == c_id, cts);

                if (category is null) 
                    return Errors.General.NotFound(c_id).ToErrorList();

                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// метод linq для вытягивания всгео количества категорий из бд
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>int <-- .CountAsync() </returns>
        public async Task<int>
            GetTotalCount(CancellationToken cancellationToken = default)
        {
            try
            {
                int count = await _dbContext.Categories.CountAsync();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Метод для вытягивания всех категорий из бд (с реализацией Linq)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> IReadOnlyList<Category> <-- .ToListAsync() </returns>
        public async Task<IReadOnlyList<Category>> GetTotalData(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.Categories.ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}