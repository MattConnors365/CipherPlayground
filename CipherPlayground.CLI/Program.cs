using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using static CipherPlayground.CLI.Logic;

namespace CipherPlayground.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var Input = GetUserInput<string>("Starting encryption, please enter the plaintext: ");
            var Key = GetUserInput<int>("Please enter the key [a whole number]: ");
            var Mode = GetUserInput<CipherMode>("Please enter the cipher mode [Strict, Loose, Preserve]: ");
            Console.WriteLine($"Input: {Input}");

            string Ciphertext = CaesarCipher.Encrypt(Input, Key, Mode);
            Console.WriteLine($"Ciphertext: {Ciphertext}");
            string DecryptedText = CaesarCipher.Decrypt(Ciphertext, Key, Mode);
            Console.WriteLine($"Decrypted text: {DecryptedText}");
        }
    }
}
