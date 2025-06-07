using System.Text;

namespace CipherPlayground.Library
{
    public class AtbashCipher
    {
        public static string Use(string input, Common.CipherMode mode = Common.Defaults.DefaultMode)
        {
            char[] alphabet = Common.Defaults.DefaultAlphabet;
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
                    switch (mode)
                    {
                        case Common.CipherMode.Strict:
                            throw new Exception($"The plaintext must not contain invalid characters. '{c}' is not valid");
                        case Common.CipherMode.Loose:
                            continue;
                        case Common.CipherMode.Preserve:
                            result.Append(c);
                            break;
                    }
                }
            }
            return result.ToString();
        }
    }
}
