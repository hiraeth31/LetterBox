using CSharpFunctionalExtensions;
using LetterBox.Domain.ArticlesManagement;
using LetterBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterBox.Application.Categories.Queries
{
    public class GetCategoryByIdHandler
    {
        private readonly ICategoriesRepository categoriesRepository;

        public GetCategoryByIdHandler(ICategoriesRepository categoriesRepository)
        {
             this.categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// метод хендлер для вытягивания категории по id (guid)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Category, ErrorList>> Handle(
            Guid id, CancellationToken cts = default)
        {
            var categoryResult = await categoriesRepository.GetById(id, cts);

            if (categoryResult.IsFailure)
                return Errors.General.Failure().ToErrorList();

            return categoryResult.Value;
        }


    }
}
