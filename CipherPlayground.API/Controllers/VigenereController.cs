using Microsoft.AspNetCore.Mvc;
using CipherPlayground.Library;
using CipherPlayground.API.Models;

namespace CipherPlayground.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VigenereController : ControllerBase
    {
        [HttpGet("info")]
        public IActionResult Info()
        {
            var info = new
            {
                Description = "Vigenère maps every character of a text to another letter using a key.",
                Endpoints = new[]
                {
                    new { Method = "GET", Path = "/info", Description = "Get information about the Vigenère cipher", RequiredFields = new[] { "None" } },
                    new { Method = "POST", Path = "/encrypt", Description = "Encrypt text", RequiredFields = new[] { "Text", "Key" } },
                    new { Method = "POST", Path = "/decrypt", Description = "Decrypt text", RequiredFields = new[] { "Text", "Key" } },
                }
            };
            return Ok(info);
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] VigenereRequest request)
        {
            var result = VigenereCipher.Encrypt(
                request.Text!,
                request.Key!,
                request.PreserveWhitespace,
                request.Mode
            );
            return Ok(result);
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] VigenereRequest request)
        {
            var result = VigenereCipher.Decrypt(
                request.Text,
                request.Key,
                request.PreserveWhitespace,
                request.Mode
            );
            return Ok(result);
        }
    }
}
