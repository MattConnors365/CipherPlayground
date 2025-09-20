using System.Text;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Library
{
    public class VigenereCipher
    {
        private static readonly char[] alphabet = Defaults.DefaultAlphabet;
        public const string defaultPlaintext = Defaults.DefaultPlaintext;
        public const string defaultKey = "KEY";
        private static readonly int alphabetLength = alphabet.Length;

        private static int GetShiftedIndex(int textIndex, int keyIndex, int alphabetLength, bool encrypt)
        {
            return encrypt
                ? (textIndex + keyIndex) % alphabetLength
                : (textIndex - keyIndex + alphabetLength) % alphabetLength;
        }

        public static string Encrypt(string plaintext = defaultPlaintext, string key = defaultKey, CipherMode mode = Defaults.DefaultMode)
        {
            int keyCounter = 0;
            StringBuilder ciphertext = new();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char c = char.ToUpper(plaintext[i]);

                if (alphabet.Contains(c))
                {
                    char k = char.ToUpper(key[keyCounter % key.Length]);
                    keyCounter++;

                    int textIndex = Array.IndexOf(alphabet, c);
                    int keyIndex = Array.IndexOf(alphabet, k);

                    ciphertext.Append(alphabet[GetShiftedIndex(textIndex, keyIndex, alphabetLength, true)]);
                }
                else
                {
                    HandleNonAlphabetic(c, mode, ciphertext);
                }
            }

            return ciphertext.ToString();
        }

        public static string Decrypt(string ciphertext, string key = defaultKey, CipherMode mode = Defaults.DefaultMode)
        {
            int keyCounter = 0;
            StringBuilder plaintext = new();

            for (int i = 0; i < ciphertext.Length; i++)
            {
                char c = char.ToUpper(ciphertext[i]);

                if (alphabet.Contains(c))
                {
                    char k = char.ToUpper(key[keyCounter % key.Length]);
                    keyCounter++;

                    int textIndex = Array.IndexOf(alphabet, c);
                    int keyIndex = Array.IndexOf(alphabet, k);

                    plaintext.Append(alphabet[GetShiftedIndex(textIndex, keyIndex, alphabetLength, false)]);
                }
                else
                {
                    HandleNonAlphabetic(c, mode, plaintext);
                }
            }

            return plaintext.ToString();
        }
    }
}
