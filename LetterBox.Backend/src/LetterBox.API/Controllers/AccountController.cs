using LetterBox.Application.Accounts.LoginUser;
using LetterBox.Application.Accounts.RegisterUser;
using LetterBox.Contracts.Requests;
using LetterBox.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    public static class Permissions
    {
        public static class Articles
        {
            public const string Create = "articles.create";
        }

        public static class Categories
        {
            public const string Create = "categories.create";
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Permission("test.admin")]
        [HttpPost("admin")]
        public IActionResult TestAdmin()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("user")]
        public IActionResult TestUser()
        {
            return Ok();
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
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest request,
            [FromServices] LoginUserHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);

            return Ok(result.Value);
        }
    }
}
