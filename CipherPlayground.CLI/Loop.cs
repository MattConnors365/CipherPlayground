using CipherPlayground.Library;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.CLI
{
    public class Loop
    {
        public static void Run()
        {
            try
            {
                Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static string GetHelpText()
        {
            return @"
Available Commands:
exit - Exit the application
help - Show this help text
caesar - Run the Caesar cipher functionality
a1z26 - Run the A1Z26 cipher functionality
vigenere - Run the Vigenere cipher functionality
            ";
        }
        public static void Start()
        {
            Console.WriteLine("Welcome to the Cipher Playground CLI!");
            Console.WriteLine("Type 'exit' to quit the application.");
            Console.WriteLine("Type 'help' to see available commands.");
            while (true)
            {
                try
                {
                    string command = Logic.GetUserInput<string>("Enter a command: ").ToLower();
                    switch (command)
                    {
                        case "exit":
                            Environment.Exit(0);
                            break;
                        case "help":
                            Console.WriteLine(GetHelpText());
                            break;
                        case "caesar":
                            RunCaesar();
                            break;
                        case "a1z26":
                            RunA1Z26();
                            break;
                        case "vigenere":
                            RunVigenere();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        public static void RunCaesar()
        {
            string[] acceptedCaesarMethods = ["encrypt", "decrypt", "bruteforce", "back"];
            string method = string.Empty;
            while (true)
            {
                Console.WriteLine($"What you can do: {string.Join(", ", acceptedCaesarMethods)}");
                method = Logic.GetUserInput<string>("Choose a method: ").ToLower();
                if (acceptedCaesarMethods.Contains(method)) { break; }
                else { Console.WriteLine("Invalid method. Please choose one of the accepted methods."); }
            }

            switch (method)
            {
                case "back":
                    Console.WriteLine("Returning to main menu...");
                    return;
                case "encrypt":
                    Console.WriteLine("Starting Cipher Playground...");
                    var inputEncrypt = Logic.GetUserInput<string>("Enter the plaintext: ");
                    var keyEncrypt = Logic.GetUserInput<int>("Enter the key (whole number): ");
                    var modeEncrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve): ");
                    var echoBack = Logic.GetUserInput<bool>("Echo back the decrypted ciphertext? (true/false): ", true);
                    string ciphertext = CaesarCipher.Encrypt(inputEncrypt, keyEncrypt, modeEncrypt);
                    Console.WriteLine($"Ciphertext: {ciphertext}");
                    if (echoBack)
                    {
                        Console.WriteLine($"Decrypted text: {CaesarCipher.Decrypt(ciphertext, keyEncrypt, modeEncrypt)}");
                    }
                    return;
                case "decrypt":
                    var inputDecrypt = Logic.GetUserInput<string>("Enter the ciphertext: ");
                    var keyDecrypt = Logic.GetUserInput<int>("Enter the key (whole number): ");
                    var modeDecrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve): ");
                    Console.WriteLine($"Decrypted text: {CaesarCipher.Decrypt(inputDecrypt, keyDecrypt, modeDecrypt)}");
                    return;
                case "bruteforce":
                    var inputBruteForce = Logic.GetUserInput<string>("Enter the ciphertext to brute-force: ");
                    var modeBruteForce = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve): ");
                    Console.WriteLine("\nBrute-force output:");
                    foreach (var result in CaesarCipher.BruteForce(inputBruteForce, modeBruteForce))
                    {
                        Console.WriteLine(result);
                    }
                    return;
            }
        }
        public static void RunA1Z26()
        {
            string[] acceptedA1Z26Methods = ["encrypt", "decrypt", "back"];
            string method = string.Empty;
            while (true)
            {
                Console.WriteLine($"What you can do: {string.Join(", ", acceptedA1Z26Methods)}");
                method = Logic.GetUserInput<string>("Choose a method: ").ToLower();
                if (acceptedA1Z26Methods.Contains(method)) { break; }
                else { Console.WriteLine("Invalid method. Please choose one of the accepted methods."); }
            }
            switch (method)
            {
                case "back":
                    Console.WriteLine("Returning to main menu...");
                    return;
                case "encrypt":
                    Console.WriteLine("Starting A1Z26 Cipher Playground...");
                    var inputEncrypt = Logic.GetUserInput<string>("Enter the plaintext: ", defaultValue: Defaults.DefaultPlaintext);
                    var modeEncrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve) ", defaultValue: Defaults.DefaultMode);
                    var charDelimiterEncrypt = Logic.GetUserInput<string>("Enter the character delimiter (between numbers): ", defaultValue: A1Z26Cipher.defaultCharDelimiter,acceptWhitespaceOnly: true);
                    var wordDelimiterEncrypt = Logic.GetUserInput<string>("Enter the word delimiter (between words): ", defaultValue: A1Z26Cipher.defaultWordDelimiter,acceptWhitespaceOnly: true);
                    var echoBack = Logic.GetUserInput<bool>("Echo back the decrypted ciphertext? (true/false): ", defaultValue: true);

                    string ciphertext = A1Z26Cipher.Encrypt(inputEncrypt, charDelimiterEncrypt, wordDelimiterEncrypt, modeEncrypt);
                    Console.WriteLine($"Ciphertext: {ciphertext}");
                    if (echoBack)
                    {
                        Console.WriteLine($"Decrypted text: {A1Z26Cipher.Decrypt(ciphertext, charDelimiterEncrypt, wordDelimiterEncrypt, modeEncrypt)}");
                    }
                    return;
                case "decrypt":
                    var inputDecrypt = Logic.GetUserInput<string>("Enter the ciphertext ", defaultValue: A1Z26Cipher.defaultCiphertext);
                    var modeDecrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve) ", defaultValue: Defaults.DefaultMode);
                    var charDelimiterDecrypt = Logic.GetUserInput<string>("Enter the character delimiter (between numbers): ", defaultValue: A1Z26Cipher.defaultCharDelimiter,acceptWhitespaceOnly: true);
                    var wordDelimiterDecrypt = Logic.GetUserInput<string>("Enter the word delimiter (between words): ", defaultValue: A1Z26Cipher.defaultWordDelimiter, acceptWhitespaceOnly: true);
                    Console.WriteLine($"Decrypted text: {A1Z26Cipher.Decrypt(inputDecrypt, charDelimiterDecrypt, wordDelimiterDecrypt, modeDecrypt)}");
                    return;
            }
        }
        public static void RunVigenere()
        {
            string[] acceptedVigenereMethods = ["encrypt", "decrypt", "back"];
            string method = string.Empty;
            while (true)
            {
                Console.WriteLine($"What you can do: {string.Join(", ", acceptedVigenereMethods)}");
                method = Logic.GetUserInput<string>("Choose a method: ").ToLower();
                if (acceptedVigenereMethods.Contains(method)) { break; }
                else { Console.WriteLine("Invalid method. Please choose one of the accepted methods."); }
            }
            switch (method)
            {
                case "back":
                    Console.WriteLine("Returning to main menu...");
                    return;
                case "encrypt":
                    Console.WriteLine("Starting Vigenere Cipher Playground...");
                    var inputEncrypt = Logic.GetUserInput<string>("Enter the plaintext: ", defaultValue: Defaults.DefaultPlaintext);
                    var keyEncrypt = Logic.GetUserInput<string>("Enter the key: ", defaultValue: VigenereCipher.defaultKey);
                    var preserveWhitespaceEncrypt = Logic.GetUserInput<bool>("Preserve whitespace? (true/false): ", defaultValue: false);
                    var modeEncrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve): ", defaultValue: Defaults.DefaultMode);
                    var echoBack = Logic.GetUserInput<bool>("Echo back the decrypted ciphertext? (true/false): ", defaultValue: true);

                    string ciphertext = VigenereCipher.Encrypt(inputEncrypt, keyEncrypt, preserveWhitespaceEncrypt, modeEncrypt);
                    Console.WriteLine($"Ciphertext: {ciphertext}");
                    if (echoBack)
                    {
                        Console.WriteLine($"Decrypted text: {VigenereCipher.Decrypt(ciphertext, keyEncrypt, preserveWhitespaceEncrypt, modeEncrypt)}");
                    }
                    return;
                case "decrypt":
                    var inputDecrypt = Logic.GetUserInput<string>("Enter the ciphertext: ", defaultValue: VigenereCipher.defaultPlaintext);
                    var keyDecrypt = Logic.GetUserInput<string>("Enter the key: ", defaultValue: VigenereCipher.defaultKey);
                    var preserveWhitespaceDecrypt = Logic.GetUserInput<bool>("Preserve whitespace? (true/false): ", defaultValue: false);
                    var modeDecrypt = Logic.GetUserInput<CipherMode>("Enter the cipher mode (Strict, Loose, Preserve): ", defaultValue: Defaults.DefaultMode);
                    Console.WriteLine($"Decrypted text: {VigenereCipher.Decrypt(inputDecrypt, keyDecrypt, preserveWhitespaceDecrypt, modeDecrypt)}");
                    return;
            }
        }
    }
}
