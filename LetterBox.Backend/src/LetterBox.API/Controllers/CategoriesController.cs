using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Categories.AddCategory;
using LetterBox.Contracts.Requests;
using LetterBox.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
       // [Permission("category.create")]
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] AddCategoryHandler handler,
            [FromBody] AddCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }
    }
}
