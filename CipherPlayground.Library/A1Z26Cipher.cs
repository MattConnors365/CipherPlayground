using static CipherPlayground.Library.Common;

namespace CipherPlayground.Library
{
    public class A1Z26Cipher
    {
        private static char[] alphabet = Defaults.DefaultAlphabet;
        public const string defaultCharDelimiter = "-";
        public const string defaultWordDelimiter = " ";
        public const string defaultCiphertext = "8-5-12-12-15";
        public static string Encrypt(string plaintext = Defaults.DefaultPlaintext, 
            string charDelimiter = defaultCharDelimiter, string wordDelimiter = defaultWordDelimiter, CipherMode mode = Defaults.DefaultMode)
        {
            plaintext = plaintext.ToUpper();
            string result = string.Empty;
            for (int i = 0; i < plaintext.Length; i++)
            {
                char currentChar = plaintext[i];
                if (alphabet.Contains(currentChar))
                {
                    int currentCharIndex = Array.IndexOf(alphabet, currentChar);
                    result += (currentCharIndex + 1).ToString() + charDelimiter;
                }
                else if (currentChar == ' ')
                {
                    result += wordDelimiter;
                }
                else
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
            if (result.Length > 0 && result.EndsWith(charDelimiter))
            {
                result = result[..^charDelimiter.Length];
            }
            if (result.Length > 0)
            {
                result = result.Replace(charDelimiter + wordDelimiter, wordDelimiter);
            }
            return result;
        }
        public static string Decrypt(string ciphertext = defaultCiphertext, 
            string charDelimiter = defaultCharDelimiter, string wordDelimiter = defaultWordDelimiter, CipherMode mode = Defaults.DefaultMode)
        {
            string[] tokens = ciphertext.Split([charDelimiter, wordDelimiter], StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;
            int index = 0;

            while (index < tokens.Length)
            {
                string token = tokens[index];

                // Check if this token is a number
                if (int.TryParse(token, out int number))
                {
                    int alphabetIndex = number - 1;

                    if (alphabetIndex >= 0 && alphabetIndex < alphabet.Length)
                    {
                        result += alphabet[alphabetIndex];
                    }
                    else
                    {
                        switch (mode)
                        {
                            case CipherMode.Strict:
                                throw new Exception($"The number {number} is out of valid range.");
                            case CipherMode.Loose:
                                // skip
                                break;
                            case CipherMode.Preserve:
                                result += token;
                                break;
                        }
                    }
                }
                else if (ciphertext.Contains(wordDelimiter) && ciphertext.IndexOf(wordDelimiter) == ciphertext.IndexOf(token)) // crude check for word break
                {
                    result += ' ';
                }
                else
                {
                    switch (mode)
                    {
                        case CipherMode.Strict:
                            throw new Exception($"Invalid token '{token}' in ciphertext.");
                        case CipherMode.Loose:
                            break;
                        case CipherMode.Preserve:
                            result += token;
                            break;
                    }
                }

                index++;
            }
            return result;
        }
    }
}
