using LetterBox.Domain.ArticlesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterBox.Application.Categories.GetCategory
{
    /// <summary>
    /// handler с методом для вытягивания всех данных о категориях из бд
    /// </summary>
    public class GetTotalDataCategoriesHandler
    {
        public ICategoriesRepository categoriesRepository;

        public GetTotalDataCategoriesHandler(ICategoriesRepository categoriesRepository)
        {
             this.categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Метод get для вытягивания всех данных о категориях из бд
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> возврат - IReadOnlyList<Category> </returns>
        public async Task<IReadOnlyList<Category>> Handle(CancellationToken cancellationToken = default)
        {
            return await categoriesRepository.GetTotalData(cancellationToken);
        }
    }
}
