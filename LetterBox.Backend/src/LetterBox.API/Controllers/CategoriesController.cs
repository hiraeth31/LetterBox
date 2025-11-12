using LetterBox.API.EndpointResults;
using LetterBox.Application.Categories.AddCategory;
using LetterBox.Contracts.Requests;
using LetterBox.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
