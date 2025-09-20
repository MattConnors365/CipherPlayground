using System.Text;

namespace CipherPlayground.Library
{
    public class Common
    {
        public static class Defaults
        {
            public static readonly char[] DefaultAlphabet = ['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'];
            public const string DefaultPlaintext = "HELLO";
            public const CipherMode DefaultMode = CipherMode.Preserve;
        }
        public enum CipherMode
        {
            Strict,
            Loose,
            Preserve
        }
        public static void HandleNonAlphabetic(char c, CipherMode mode, StringBuilder result)
        {
            switch (mode)
            {
                case CipherMode.Strict:
                    throw new Exception($"Invalid character '{c}'");
                case CipherMode.Loose:
                    // skip character
                    break;
                case CipherMode.Preserve:
                    result.Append(c);
                    break;
            }
        }
        public static void HandleInvalidToken(string token, CipherMode mode, StringBuilder result)
        {
            switch (mode)
            {
                case CipherMode.Strict:
                    throw new Exception($"Invalid token '{token}'");
                case CipherMode.Loose:
                    // skip token
                    break;
                case CipherMode.Preserve:
                    result.Append(token);
                    break;
            }
        }

    }
}
