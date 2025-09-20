# This is a simple demonstration of using the API of the project with a Python CLI application.

from cipher_client import CaesarCipherClient, VigenereCipherClient, A1Z26CipherClient, AtbashCipherClient

def main():
    print("This is a simple demonstration of using the API of the project with a Python CLI application.")
    print("You can use the cipher_client module to interact with the API.")
    print("For example, you can call the encrypt function to encrypt a message.")

    switch = input("Cipher (caesar, vigenere, a1z26, atbash): ").strip().lower()

    match switch:
        case "caesar":
            text = input("Enter text: ")
            operation = input("Encrypt, Decrypt, or Bruteforce (e/d/b): ").strip().lower()
            match operation:
                case "e" | "encrypt":
                    key = int(input("Enter key (integer): "))
                    mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
                    print(CaesarCipherClient.encrypt(text, key, mode))
                case "d" | "decrypt":
                    key = int(input("Enter key (integer): "))
                    mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
                    print(CaesarCipherClient.decrypt(text, key, mode))
                case "b" | "bruteforce":
                    mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
                    print(CaesarCipherClient.brute_force(text, mode))
                case _:
                    print("Invalid operation. Please choose from encrypt, decrypt, or bruteforce.")
        case "vigenere":
            text = input("Enter text: ")
            key = input("Enter key: ")
            mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
            operation = input("Encrypt or Decrypt (e/d): ").strip().lower()
            match operation:
                case "e" | "encrypt":
                    print (VigenereCipherClient.encrypt(text, key, mode))
                case "d" | "decrypt":
                    print (VigenereCipherClient.decrypt(text, key, mode))
        case "a1z26":
            text = input("Enter text: ")
            mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
            character_delimiter = input("Character delimiter (default '-'): ") or "-"
            word_delimiter = input("Word delimiter (default ' '): ") or " "
            operation = input("Encrypt or Decrypt (e/d): ").strip().lower()
            match operation:
                case "e" | "encrypt":
                    print(A1Z26CipherClient.encrypt(text, mode, character_delimiter, word_delimiter))
                case "d" | "decrypt":
                    print(A1Z26CipherClient.decrypt(text, mode, character_delimiter, word_delimiter))
        case "atbash":
            text = input("Enter text: ")
            mode = int(input("Mode (0 for Strict, 1 for Loose, 2 for Preserve): "))
            print(AtbashCipherClient.use(text, mode))
        case _:
            print("Invalid cipher type. Please choose from caesar, vigenere, or a1z26.")

if __name__ == "__main__":
    main()
