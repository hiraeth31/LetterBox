using LetterBox.API.EndpointResults;
using LetterBox.Application.Accounts.LoginUser;
using LetterBox.Application.Accounts.RefreshTokens;
using LetterBox.Application.Accounts.RegisterUser;
using LetterBox.Application.Accounts.Responses;
using LetterBox.Contracts.Requests;
using LetterBox.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        [HttpPost("admin")]
        [Permission(Permissions.Articles.Delete)]
        public IActionResult Admin()
        {
            return Ok("admin");
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            [FromServices] RegisterUserHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<EndpointResult<LoginResponse>> Login(
            [FromBody] LoginUserRequest request,
            [FromServices] LoginHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);
            //if (result.IsFailure)
            //{
            //    return result;
            //}

            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Expires = DateTime.UtcNow.AddDays(7)
            //};

            HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());//, cookieOptions

            return result;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(
            [FromServices] RefreshTokensHandler handler,
            CancellationToken cancellationToken)
        {
            if(!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                return Unauthorized();

            var command = new RefreshTokensCommand(Guid.Parse(refreshToken));

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest();
            }

            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Expires = DateTime.UtcNow.AddDays(7)
            //};

            HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());

            return Ok(result.Value);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            // Удалить куки
            HttpContext.Response.Cookies.Delete("refreshToken");
            
            // todo Удалить рефреш токен из бд

            return Ok();
        }
    }
}
