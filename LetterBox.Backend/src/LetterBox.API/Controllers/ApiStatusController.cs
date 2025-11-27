
using Microsoft.AspNetCore.Mvc;

namespace LetterBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiStatusController: ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API is working now";
        }

        /// <summary>
        /// api метод <object> для проверки статуса
        /// backend'a (без проверки на реализацию)
        /// </summary>
        /// <returns> Task<IActionResult> (_status, datetime.utcNow, _message) </returns>
        [HttpGet("status")]
        public async Task<IActionResult> PingStatus()
        {
            return Ok(new
            {
                status = "OK",
                timestamp = DateTime.UtcNow,
                message = "API доступен ✅ | API status is avaliable ✅"
            });
        }
    }
}
