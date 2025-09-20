using System.Text;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Library
{
    public class AtbashCipher
    {
        public static string Use(string input, CipherMode mode = Defaults.DefaultMode)
        {
            char[] alphabet = Defaults.DefaultAlphabet;
            char[] reversedAlphabet = [..alphabet.Reverse()];
            StringBuilder result = new();
            input = input.ToUpperInvariant(); // Normalize to uppercase for consistency
            foreach (char c in input)
            {
                if (alphabet.Contains(c))
                {
                    result.Append(reversedAlphabet[Array.IndexOf(alphabet, c)]);
                }
                else
                {
                    HandleNonAlphabetic(c, mode, result);
                }
            }
            return result.ToString();
        }
    }
}
