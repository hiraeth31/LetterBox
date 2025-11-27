
using CSharpFunctionalExtensions;
using LetterBox.Domain.Common;

namespace LetterBox.Application.Categories.GetCategory
{
    /// <summary>
    /// handler с методом для вытягивания всего количества категорий из бд
    /// </summary>
    public class GetTotalCountCategoriesHandler
    {
        private readonly ICategoriesRepository categoriesRepository;

        public GetTotalCountCategoriesHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Метод get Для вытягивания всего количества категорий из бд
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Result<int, ErrorList> <-- int </returns>
        public async Task<Result<int, ErrorList>> Handle(CancellationToken cancellationToken = default)
        {
            return await categoriesRepository.GetTotalCount(cancellationToken);
        }
    }
}