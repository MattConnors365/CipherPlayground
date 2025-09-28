using CipherPlayground.Library;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Tests.LibraryTests
{
    public class VigenereCipherTests
    {
        // Encrypt / Decrypt known plaintext/ciphertext
        [Theory]
        [InlineData("HELLO", "KEY", "RIJVS")]
        [InlineData("WORLD", "KEY", "GSPVH")]
        public void Encrypt_KnownPlaintext_ReturnsExpected(string plaintext, string key, string expected)
        {
            string result = VigenereCipher.Encrypt(plaintext, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("RIJVS", "KEY", "HELLO")]
        [InlineData("GSPVH", "KEY", "WORLD")]
        public void Decrypt_KnownCiphertext_ReturnsExpected(string ciphertext, string key, string expected)
        {
            string result = VigenereCipher.Decrypt(ciphertext, key);
            Assert.Equal(expected, result);
        }

        // Encrypt / Decrypt empty string
        [Fact]
        public void Encrypt_EmptyString_ReturnsEmpty() =>
            Assert.Equal("", VigenereCipher.Encrypt(""));

        [Fact]
        public void Decrypt_EmptyString_ReturnsEmpty() =>
            Assert.Equal("", VigenereCipher.Decrypt(""));

        // Loose mode: removes all non-alphabetic characters
        [Theory]
        [InlineData("HELLO!", CipherMode.Loose, "RIJVS")]
        [InlineData("WORLD 123", CipherMode.Loose, "GSPVH")]
        public void Encrypt_LooseMode_RemovesNonAlphabetic(string plaintext, CipherMode mode, string expected)
        {
            string result = VigenereCipher.Encrypt(plaintext, VigenereCipher.defaultKey, mode);
            Assert.Equal(expected, result);
        }

        // Preserve mode: keeps non-alphabetic characters in place
        [Theory]
        [InlineData("HELLO!", CipherMode.Preserve, "RIJVS!")]
        [InlineData("WORLD 123", CipherMode.Preserve, "GSPVH 123")]
        public void Encrypt_PreserveMode_KeepsNonAlphabetic(string plaintext, CipherMode mode, string expected)
        {
            string result = VigenereCipher.Encrypt(plaintext, VigenereCipher.defaultKey, mode);
            Assert.Equal(expected, result);
        }

        // Encrypt / Decrypt repeats key if plaintext longer
        [Fact]
        public void Encrypt_PlaintextLongerThanKey_RepeatsKeyCorrectly()
        {
            string plaintext = "VIGENERECIPHER";
            string key = "SECRET";
            string result = VigenereCipher.Encrypt(plaintext, key);
            string decrypted = VigenereCipher.Decrypt(result, key);
            Assert.Equal(plaintext, decrypted);
        }

        // Strict mode: throws exception on invalid characters
        [Theory]
        [InlineData("HELLO!", CipherMode.Strict)]
        [InlineData("WORLD123", CipherMode.Strict)]
        public void Encrypt_WithInvalidCharAndStrict_ThrowsException(string plaintext, CipherMode mode)
        {
            Assert.Throws<ArgumentException>(() => VigenereCipher.Encrypt(plaintext, VigenereCipher.defaultKey, mode));
        }

        [Theory]
        [InlineData("HELLO!", CipherMode.Strict)]
        [InlineData("WORLD123", CipherMode.Strict)]
        public void Decrypt_WithInvalidCharAndStrict_ThrowsException(string ciphertext, CipherMode mode)
        {
            Assert.Throws<ArgumentException>(() => VigenereCipher.Decrypt(ciphertext, VigenereCipher.defaultKey, mode));
        }

        // Round-trip encryption/decryption returns original text
        [Theory]
        [InlineData("HELLO WORLD", "KEY", CipherMode.Preserve)]
        [InlineData("VIGENERECIPHER", "SECRET", CipherMode.Loose)]
        public void EncryptThenDecrypt_ReturnsOriginal(string plaintext, string key, CipherMode mode)
        {
            string ciphertext = VigenereCipher.Encrypt(plaintext, key, mode);
            string decrypted = VigenereCipher.Decrypt(ciphertext, key, mode);

            // Normalize plaintext for Loose mode (removes all non-alphabetic characters)
            if (mode == CipherMode.Loose)
                plaintext = new string(plaintext.ToUpper().Where(c => Defaults.DefaultAlphabet.Contains(c)).ToArray());
            else
                plaintext = plaintext.ToUpper();

            Assert.Equal(plaintext, decrypted);
        }
    }
}
