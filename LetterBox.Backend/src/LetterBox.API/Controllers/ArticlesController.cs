using LetterBox.API.EndpointResults;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Articles.GetArticle;
using LetterBox.Contracts.Requests;
using LetterBox.Domain.ArticlesManagement;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IReadOnlyList<Article>> GetAll(
            [FromServices] GetAllArticlesHandler handler,
            CancellationToken cancellationToken)
        {
            return await handler.Handle(cancellationToken);
        }
        
    }
}
