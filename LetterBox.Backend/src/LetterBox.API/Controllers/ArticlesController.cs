using LetterBox.Application.Articles.AddArticle;
using LetterBox.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] AddArticleHandler handler,
            [FromBody] AddArticleRequest request,
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
