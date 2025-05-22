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

            string result = string.Empty;

            for (int i = 0; i < plaintext.Length; i++)
            {
                char currentChar = plaintext[i];
                if (alphabet.Contains(currentChar))
                {
                    int currentCharIndex = Array.IndexOf(alphabet, currentChar);
                    int newIndex = (currentCharIndex + key) % alphabetLength;
                    if (newIndex < 0) { newIndex += alphabetLength; }
                    result += alphabet[newIndex];
                } else
                {
                    switch (mode)
                    {
                        case CipherMode.Strict:
                            throw new Exception($"The plaintext must not contain invalid characters. '{currentChar}' is not valid");
                        case CipherMode.Loose:
                            continue;
                        case CipherMode.Preserve:
                            result += currentChar;
                            break;
                    }
                }
            }

            return result;
        }
        public static string Decrypt(string ciphertext, int key, CipherMode mode = Defaults.DefaultMode)
        {
            return Encrypt(ciphertext, (key * -1), mode);
        }
        public static IEnumerable<string> BruteForce(string ciphertext, CipherMode mode = Defaults.DefaultMode)
        {
            int alphabetLength = alphabet.Length;

            for (int key = 1; key < alphabetLength; key++) // skip 0 (original text)
            {
                string attempt = Encrypt(ciphertext, key, mode);
                yield return $"Key {key:D2}: {attempt}";
            }
        }

    }
}
