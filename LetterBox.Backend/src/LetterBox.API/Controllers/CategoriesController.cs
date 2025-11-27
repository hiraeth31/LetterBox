using LetterBox.API.EndpointResults;
using LetterBox.Application.Categories.AddCategory;
using LetterBox.Application.Categories.GetCategory;
using LetterBox.Contracts.Requests;
using LetterBox.Domain.ArticlesManagement;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        // [Permission("category.create")]
        [HttpPost]
        public async Task<EndpointResult<Guid>> Create(
            [FromServices] AddCategoryHandler handler,
            [FromBody] AddCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            return await handler.Handle(command, cancellationToken);
        }

        /// <summary>
        /// HttpGet метод для API контроллера. Вытягивание всего количества статей из бд
        /// </summary>
        /// <param name="handler"> класс с описанным методом (ICategoriesRepository) </param>
        /// <param name="cancellationToken"></param>
        /// <returns> возврат - EndpointResult<int> <-- Result<int, ErrorList> </returns>
        [HttpGet("count")]
        public async Task<EndpointResult<int>> GetTotalCountCategories(
            [FromServices] GetTotalCountCategoriesHandler handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.Handle(cancellationToken);
        }

        /// <summary>
        /// HttpGet метод для API контроллера. Вытягивание всех данных о категориях из бд
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> IReadOnlyList<Category> </returns>
        [HttpGet("data")]
        public async Task<IReadOnlyList<Category>> GetTotalDataCategories(
            [FromServices] GetTotalDataCategoriesHandler handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.Handle(cancellationToken);
        }

    }


}
