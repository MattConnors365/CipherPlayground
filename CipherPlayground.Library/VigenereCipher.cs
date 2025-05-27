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
        private static int GetShiftedIndex(int plaintextIndex, int keyIndex, int alphabetLength, bool encrypt)
        {
            return encrypt
                ? (plaintextIndex + keyIndex) % alphabetLength // for encryption
                : (plaintextIndex - keyIndex + alphabetLength) % alphabetLength; // for decryption
        }
        public static string Encrypt(string plaintext = defaultPlaintext, string key = defaultKey, bool preserveWhitespace = false, CipherMode mode = Defaults.DefaultMode)
        {
            int keyCounter = 0;

            StringBuilder ciphertext = new();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char p = char.ToUpper(plaintext[i]);

                if (alphabet.Contains(p))
                {
                    char k = char.ToUpper(key[keyCounter % key.Length]);
                    keyCounter++;

                    int pIndex = Array.IndexOf(alphabet, p);
                    int kIndex = Array.IndexOf(alphabet, k);

                    ciphertext.Append(alphabet[GetShiftedIndex(pIndex, kIndex, alphabetLength, true)]);
                }
                else
                {
                    switch (mode)
                    {
                        case CipherMode.Strict:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                ciphertext.Append(plaintext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            throw new ArgumentException($"Character '{p}' is not in the alphabet.");
                        case CipherMode.Loose:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                ciphertext.Append(plaintext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            continue; // skip characters not in the alphabet
                        case CipherMode.Preserve:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                ciphertext.Append(plaintext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            else if (char.IsWhiteSpace(p) && !preserveWhitespace)
                            {
                                continue; // skip whitespace characters if not preserving whitespace
                            }
                            break;
                    }
                }
            }
            return ciphertext.ToString();
        }
        public static string Decrypt(string ciphertext, string key = defaultKey, bool preserveWhitespace = false, CipherMode mode = Defaults.DefaultMode)
        {
            int keyCounter = 0;

            StringBuilder plaintext = new();

            for (int i = 0; i < ciphertext.Length; i++)
            {
                char p = char.ToUpper(ciphertext[i]);

                if (alphabet.Contains(p))
                {
                    char k = char.ToUpper(key[keyCounter % key.Length]);
                    keyCounter++;

                    int pIndex = Array.IndexOf(alphabet, p);
                    int kIndex = Array.IndexOf(alphabet, k);

                    plaintext.Append(alphabet[GetShiftedIndex(pIndex, kIndex, alphabetLength, false)]);
                }
                else
                {
                    switch (mode)
                    {
                        case CipherMode.Strict:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                plaintext.Append(ciphertext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            throw new ArgumentException($"Character '{p}' is not in the alphabet.");
                        case CipherMode.Loose:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                plaintext.Append(ciphertext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            continue; // skip characters not in the alphabet
                        case CipherMode.Preserve:
                            if (char.IsWhiteSpace(p) && preserveWhitespace)
                            {
                                plaintext.Append(ciphertext[i]); // preserve spaces/punctuation
                                continue;
                            }
                            else if (char.IsWhiteSpace(p) && !preserveWhitespace)
                            {
                                continue; // skip whitespace characters if not preserving whitespace
                            }
                            break;
                    }
                }
            }
            return plaintext.ToString();
        }
    }
}
