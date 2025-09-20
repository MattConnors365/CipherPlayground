using System.ComponentModel.DataAnnotations;
using CipherPlayground.Library;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.API.Models
{
    public class VigenereRequest
    {
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Key { get; set; }
        public CipherMode Mode { get; set; } = Common.Defaults.DefaultMode;
    }
}
