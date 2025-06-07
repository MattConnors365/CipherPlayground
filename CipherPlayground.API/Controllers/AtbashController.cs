using Microsoft.AspNetCore.Mvc;
using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using CipherPlayground.API.Models;

namespace CipherPlayground.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtbashController : ControllerBase
    {
        [HttpGet("info")]
        public IActionResult Info()
        {
            var info = new
            {
                Description = "Atbash reverses the alphabet and switches each character to its corresponding new position.",
                Endpoints = new[]
                {
                    new { Method = "GET", Path = "/info", Description = "Get information about the Atbash cipher", RequiredFields = new[] { "None" } },
                    new { Method = "POST", Path = "/use", Description = "Encrypt or Decrypt text", RequiredFields = new[] { "Text" } },
                }
            };
            return Ok(info);
        }

        [HttpPost("use")]
        public IActionResult Use([FromBody] AtbashRequest request)
        {
            var result = AtbashCipher.Use(
                request.Text!,
                request.Mode
            );
            return Ok(result);
        }
    }
}
