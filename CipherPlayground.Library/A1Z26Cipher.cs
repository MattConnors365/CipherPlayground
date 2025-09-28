using System.Text;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Library
{
    public class A1Z26Cipher
    {
        private static char[] alphabet = Defaults.DefaultAlphabet;
        public const string defaultCharDelimiter = "-";
        public const string defaultWordDelimiter = " ";
        public const string defaultCiphertext = "8-5-12-12-15";
        public static string Encrypt(
            string plaintext = Defaults.DefaultPlaintext,
            string charDelimiter = defaultCharDelimiter,
            string wordDelimiter = defaultWordDelimiter,
            CipherMode mode = Defaults.DefaultMode)
        {
            plaintext = plaintext.ToUpper();
            var result = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char currentChar = plaintext[i];

                if (alphabet.Contains(currentChar))
                {
                    int currentCharIndex = Array.IndexOf(alphabet, currentChar);
                    result.Append(currentCharIndex + 1).Append(charDelimiter);
                }
                else if (currentChar == ' ')
                {
                    result.Append(wordDelimiter);
                }
                else
                {
                    HandleNonAlphabetic(currentChar, mode, result);
                }
            }

            // clean up trailing delimiters
            if (result.Length > 0 && result.ToString().EndsWith(charDelimiter))
            {
                result.Length -= charDelimiter.Length;
            }
            result.Replace(charDelimiter + wordDelimiter, wordDelimiter);

            return result.ToString();
        }
        public static string Decrypt(
    string ciphertext = defaultCiphertext,
    string charDelimiter = defaultCharDelimiter,
    string wordDelimiter = defaultWordDelimiter,
    CipherMode mode = Defaults.DefaultMode)
        {
            string[] words = ciphertext.Split(wordDelimiter, StringSplitOptions.None);
            var result = new StringBuilder();

            foreach (var word in words)
            {
                string[] tokens = word.Split(charDelimiter, StringSplitOptions.RemoveEmptyEntries);

                foreach (var token in tokens)
                {
                    if (int.TryParse(token, out int number))
                    {
                        int alphabetIndex = number - 1;

                        if (alphabetIndex >= 0 && alphabetIndex < alphabet.Length)
                        {
                            result.Append(alphabet[alphabetIndex]);
                            continue;
                        }

                        // Value is numeric, but outside alphabet range
                        throw new ArgumentOutOfRangeException(
                            nameof(ciphertext),
                            $"Token '{token}' is outside valid range (1-{alphabet.Length})");
                    }
                    else
                    {
                        HandleInvalidToken(token, mode, result);
                    }
                }

                // restore space between words
                result.Append(' ');
            }

            return result.ToString().TrimEnd();
        }
    }
}
