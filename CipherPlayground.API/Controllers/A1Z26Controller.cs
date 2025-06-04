using Microsoft.AspNetCore.Mvc;
using CipherPlayground.Library;
using CipherPlayground.API.Models;

namespace CipherPlayground.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class A1Z26Controller : ControllerBase
    {
        [HttpGet("info")]
        public IActionResult Info()
        {
            var info = new
            {
                Description = "A1Z26 changes each character to its position in the alphabet.",
                Endpoints = new[]
                {
                    new { Method = "GET", Path = "/info", Description = "Get information about the A1Z26 cipher", RequiredFields = new[] { "None" } },
                    new { Method = "POST", Path = "/encrypt", Description = "Encrypt text", RequiredFields = new[] { "Text" } },
                    new { Method = "POST", Path = "/decrypt", Description = "Decrypt text", RequiredFields = new[] { "Text" } },
                }
            };
            return Ok(info);
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] A1Z26Request request)
        {
            var result = A1Z26Cipher.Encrypt(
                request.Text!,
                request.CharDelimiter,
                request.WordDelimiter,
                request.Mode
            );
            return Ok(result);
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] A1Z26Request request)
        {
            var result = A1Z26Cipher.Decrypt(
                request.Text!,
                request.CharDelimiter,
                request.WordDelimiter,
                request.Mode
            );
            return Ok(result);
        }
    }
}
