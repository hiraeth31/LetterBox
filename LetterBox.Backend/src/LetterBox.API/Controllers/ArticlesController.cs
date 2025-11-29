using LetterBox.API.EndpointResults;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Articles.Queries;
using LetterBox.Application.Articles.GetArticle;
using LetterBox.Contracts.Requests;
using LetterBox.Domain.ArticlesManagement;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("data")]
        public async Task<IReadOnlyList<Article>> Get(//EndpointResult<>
            [FromServices] GetArticleHandler handler,
            CancellationToken cancellationToken)
        {
            return await handler.Handle(cancellationToken);
        }

        /// <summary>
        /// api метод для получения статьи по guid
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="id">guid статьи</param>
        /// <param name="cts"></param>
        /// <returns>EndpointResult<Article> <-- Result<T, Error></T> </returns>
        [HttpGet ("{id:guid}")]
        public async Task<EndpointResult<Article>> GetById(
            [FromServices] GetArticleByIdHandler handler,
            [FromRoute] Guid id,
            CancellationToken cts)
        {
            return await handler.Handle(id, cts);
        }
            
        /// <summary>
        /// api метод для получения количства статей
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<EndpointResult<int>> GetCountArticles(
            [FromServices] GetArticlesCountHandler handler,
            CancellationToken cancellationToken)
        {
            return await handler.Handle(cancellationToken);
        }
        
    }
}
