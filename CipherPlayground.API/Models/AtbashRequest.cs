using System.ComponentModel.DataAnnotations;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.API.Models
{
    public class AtbashRequest
    {
        [Required]
        public string? Text { get; set; }

        public CipherMode Mode { get; set; } = Defaults.DefaultMode;
    }
}
