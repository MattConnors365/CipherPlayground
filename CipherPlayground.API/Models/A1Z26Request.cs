using System.ComponentModel.DataAnnotations;
using CipherPlayground.Library;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.API.Models
{
    public class A1Z26Request
    {
        [Required]
        public string? Text { get; set; }

        public string CharDelimiter { get; set; } = A1Z26Cipher.defaultCharDelimiter;
        public string WordDelimiter { get; set; } = A1Z26Cipher.defaultWordDelimiter;
        public CipherMode Mode { get; set; } = Defaults.DefaultMode;
    }
}
