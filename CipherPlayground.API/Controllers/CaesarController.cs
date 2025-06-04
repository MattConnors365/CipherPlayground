using Microsoft.AspNetCore.Mvc;
using CipherPlayground.Library;
using CipherPlayground.API.Models;

namespace CipherPlayground.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaesarController : ControllerBase
    {
        [HttpGet("info")]
        public IActionResult Info()
        {
            var info = new
            {
                Description = "Caesar cipher shifts letters by a key value.",
                Endpoints = new[]
                {
                    new { Method = "GET", Path = "/info", Description = "Get information about the Caesar cipher", RequiredFields = new[] { "None" } },
                    new { Method = "POST", Path = "/encrypt", Description = "Encrypt text", RequiredFields = new[] { "Text", "Key" } },
                    new { Method = "POST", Path = "/decrypt", Description = "Decrypt text", RequiredFields = new[] { "Text", "Key" } },
                    new { Method = "POST", Path = "/bruteforce", Description = "Try all keys to decrypt", RequiredFields = new[] { "Text" } }
                }
            };
            return Ok(info);
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] CaesarRequest request)
        {
            var result = CaesarCipher.Encrypt(request.Text!, request.Key, request.Mode);
            return Ok(result);
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] CaesarRequest request)
        {
            var result = CaesarCipher.Decrypt(request.Text!, request.Key, request.Mode);
            return Ok(result);
        }

        [HttpPost("bruteforce")]
        public IActionResult BruteForce([FromBody] CaesarBruteForceRequest request)
        {
            var results = CaesarCipher.BruteForce(request.Text!, request.Mode);
            return Ok(results);
        }
    }

}
