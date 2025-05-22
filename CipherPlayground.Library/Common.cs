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
    }
}
