using LetterBox.API.EndpointResults;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Articles.GetArticle;
using LetterBox.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        [HttpPost]
        public async Task<EndpointResult<Guid>> Create(
            [FromServices] AddArticleHandler handler,
            [FromBody] AddArticleRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            return await handler.Handle(command, cancellationToken);
        }

        [HttpGet("count")]
        public async Task<EndpointResult<int>> GetCountArticles(
            [FromServices] GetArticleHandler handler,
            CancellationToken cancellationToken)
        {
            return await handler.HandleTotalCount(cancellationToken);
        }
        
    }
}
