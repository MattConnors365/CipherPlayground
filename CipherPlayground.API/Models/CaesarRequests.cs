using System.ComponentModel.DataAnnotations;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.API.Models
{
    public class CaesarRequest
    {
        [Required]
        public string? Text { get; set; }
        [Required]
        public int Key { get; set; }
        public CipherMode Mode { get; set; } = Defaults.DefaultMode;
    }
    public class CaesarBruteForceRequest
    {
        [Required]
        public string? Text { get; set; }
        public CipherMode Mode { get; set; } = Defaults.DefaultMode;
    }
}
