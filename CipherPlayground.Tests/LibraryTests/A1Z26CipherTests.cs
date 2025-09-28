using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using System.Linq;
using Xunit;

namespace CipherPlayground.Tests.LibraryTests
{
    public class A1Z26CipherTests
    {
        // Encrypt / Decrypt known plaintext/ciphertext
        [Theory]
        [InlineData("HELLO", "8-5-12-12-15")]
        [InlineData("WORLD", "23-15-18-12-4")]
        [InlineData("HELLO WORLD", "8-5-12-12-15 23-15-18-12-4")]
        public void Encrypt_KnownPlaintext_ReturnsExpected(string plaintext, string expected)
        {
            string result = A1Z26Cipher.Encrypt(plaintext);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("8-5-12-12-15", "HELLO")]
        [InlineData("23-15-18-12-4", "WORLD")]
        [InlineData("8-5-12-12-15 23-15-18-12-4", "HELLO WORLD")]
        public void Decrypt_KnownCiphertext_ReturnsExpected(string ciphertext, string expected)
        {
            string result = A1Z26Cipher.Decrypt(ciphertext);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encrypt_EmptyString_ReturnsEmpty() =>
            Assert.Equal("", A1Z26Cipher.Encrypt(""));

        [Fact]
        public void Decrypt_EmptyString_ReturnsEmpty() =>
            Assert.Equal("", A1Z26Cipher.Decrypt(""));

        [Fact]
        public void Encrypt_CustomDelimiters_WorksCorrectly()
        {
            string result = A1Z26Cipher.Encrypt("HELLO WORLD", charDelimiter: ".", wordDelimiter: "|");
            Assert.Equal("8.5.12.12.15|23.15.18.12.4", result);
        }

        [Fact]
        public void Decrypt_CustomDelimiters_WorksCorrectly()
        {
            string result = A1Z26Cipher.Decrypt("8.5.12.12.15|23.15.18.12.4", charDelimiter: ".", wordDelimiter: "|");
            Assert.Equal("HELLO WORLD", result);
        }

        // Non-alphabetic characters handled according to mode
        [Theory]
        [InlineData("HELLO!", CipherMode.Loose, "8-5-12-12-15")]
        [InlineData("HELLO!", CipherMode.Preserve, "8-5-12-12-15-!")]
        public void Encrypt_NonAlphabetic_RespectsMode(string plaintext, CipherMode mode, string expected)
        {
            string result = A1Z26Cipher.Encrypt(plaintext, mode: mode);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("8-5-12-12-15-!", CipherMode.Loose, "HELLO")]
        [InlineData("8-5-12-12-15-!", CipherMode.Preserve, "HELLO!")]
        public void Decrypt_NonNumericToken_RespectsMode(string ciphertext, CipherMode mode, string expected)
        {
            string result = A1Z26Cipher.Decrypt(ciphertext, mode: mode);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("HELLO!", CipherMode.Strict)]
        public void Encrypt_StrictMode_InvalidCharacter_Throws(string plaintext, CipherMode mode)
        {
            Assert.Throws<ArgumentException>(() => A1Z26Cipher.Encrypt(plaintext, mode: mode));
        }

        [Theory]
        [InlineData("8-5-12-12-15!", CipherMode.Strict)]
        public void Decrypt_StrictMode_InvalidToken_Throws(string ciphertext, CipherMode mode)
        {
            Assert.Throws<FormatException>(() => A1Z26Cipher.Decrypt(ciphertext, mode: mode));
        }

        // Round-trip encryption/decryption
        [Theory]
        [InlineData("HELLO WORLD", CipherMode.Preserve)]
        [InlineData("HELLO WORLD!", CipherMode.Preserve)]
        [InlineData("HELLO WORLD", CipherMode.Loose)]
        [InlineData("HELLO WORLD!", CipherMode.Loose)]
        public void EncryptThenDecrypt_ReturnsOriginal(string plaintext, CipherMode mode)
        {
            string ciphertext = A1Z26Cipher.Encrypt(plaintext, mode: mode);
            string decrypted = A1Z26Cipher.Decrypt(ciphertext, mode: mode);

            if (mode == CipherMode.Loose)
            {
                // Loose mode removes non-alphabetic characters but preserves spaces
                plaintext = new string(plaintext.ToUpper().Where(c => Defaults.DefaultAlphabet.Contains(c) || c == ' ').ToArray());
            }
            else
            {
                plaintext = plaintext.ToUpper();
            }

            Assert.Equal(plaintext, decrypted);
        }
    }
}
