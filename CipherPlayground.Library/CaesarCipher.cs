using System.Text;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Library
{
    public class CaesarCipher
    {
        private static char[] alphabet = Defaults.DefaultAlphabet;
        public static string Encrypt(string plaintext, int key, CipherMode mode = Defaults.DefaultMode)
        {
            int alphabetLength = alphabet.Length;
            plaintext = plaintext.ToUpper();

            StringBuilder result = new();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char currentChar = plaintext[i];
                if (alphabet.Contains(currentChar))
                {
                    int currentCharIndex = Array.IndexOf(alphabet, currentChar);
                    int newIndex = (currentCharIndex + key) % alphabetLength;
                    if (newIndex < 0) { newIndex += alphabetLength; }
                    result.Append(alphabet[newIndex]);
                }
                else
                {
                    HandleNonAlphabetic(currentChar, mode, result);
                }
            }

            return result.ToString();
        }
        public static string Decrypt(string ciphertext, int key, CipherMode mode = Defaults.DefaultMode)
        {
            return Encrypt(ciphertext, (key * -1), mode);
        }
        public static IEnumerable<string> BruteForce(string ciphertext, CipherMode mode = Defaults.DefaultMode)
        {
            int alphabetLength = alphabet.Length;

            for (int key = 0; key < alphabetLength; key++)
            {
                string attempt = Encrypt(ciphertext, key, mode);
                yield return $"Key {key:D2}: {attempt}";
            }
        }

    }
}
