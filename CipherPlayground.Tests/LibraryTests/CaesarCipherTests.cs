using CipherPlayground.Library;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Tests.LibraryTests
{
    public class CaesarCipherTests
    {
        // Test for all modes working correctly
        [Theory]
        [InlineData("ABC XYZ!", 3, CipherMode.Loose, "DEFABC")]
        [InlineData("ABC XYZ!", 3, CipherMode.Preserve, "DEF ABC!")]
        public void Encrypt_WithValidModes_ReturnsExpected(
            string plaintext, int key, CipherMode mode, string expected)
        {
            var result = CaesarCipher.Encrypt(plaintext, key, mode);
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Encrypt_WithInvalidCharAndStrict_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                CaesarCipher.Encrypt("ABC XYZ!", 3, CipherMode.Strict));
        }

        [Theory]
        [InlineData("DEF ABC!", 3, CipherMode.Loose, "ABCXYZ")]
        [InlineData("DEF ABC!", 3, CipherMode.Preserve, "ABC XYZ!")]
        public void Decrypt_WithValidModes_ReturnsExpected(
            string ciphertext, int key, CipherMode mode, string expected)
        {
            var result = CaesarCipher.Decrypt(ciphertext, key, mode);
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Decrypt_WithInvalidCharAndStrict_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                CaesarCipher.Decrypt("DEF ABC!", 3, CipherMode.Strict));
        }

        [Fact]
        public void BruteForce_ReturnsAllKeysAndCorrectResult()
        {
            // Arrange
            string ciphertext = "DEF";
            int alphabetLength = Defaults.DefaultAlphabet.Length;

            // Act
            var results = CaesarCipher.BruteForce(ciphertext, CipherMode.Preserve).ToList();

            // Assert
            Assert.Equal(alphabetLength, results.Count);               // 26 outputs
            Assert.Contains(results, r => r.EndsWith("ABC"));          // one of them decrypts to original
            Assert.StartsWith("Key 00:", results.First());             // optional: first format check
            Assert.StartsWith($"Key {alphabetLength - 1:D2}:", results.Last()); // optional: last format check
        }

        // Make sure empty input returns empty string

        [Fact]
        public void Encrypt_EmptyString_ReturnsEmpty()
        {
            Assert.Equal("", CaesarCipher.Encrypt("", 3, CipherMode.Preserve));
        }

        [Fact]
        public void Decrypt_EmptyString_ReturnsEmpty()
        {
            Assert.Equal("", CaesarCipher.Decrypt("", 3, CipherMode.Preserve));
        }

        // Negative keys should shift backwards

        [Theory]
        [InlineData("ABC", -3, CipherMode.Preserve, "XYZ")]
        [InlineData("ABC", -1, CipherMode.Preserve, "ZAB")]
        public void Encrypt_NegativeKey_ShiftsBackwards(string plaintext, int key, CipherMode mode, string expected)
        {
            Assert.Equal(expected, CaesarCipher.Encrypt(plaintext, key, mode));
        }

        // Keys larger than the length of the alphabet need to wrap around

        [Theory]
        [InlineData("ABC", 29, CipherMode.Preserve, "DEF")] // 29 % 26 = 3
        [InlineData("XYZ", 52, CipherMode.Preserve, "XYZ")] // 52 % 26 = 0
        public void Encrypt_LargeKey_WrapsCorrectly(string plaintext, int key, CipherMode mode, string expected)
        {
            Assert.Equal(expected, CaesarCipher.Encrypt(plaintext, key, mode));
        }

        // Loose mode returns empty string when no alphabet members are in the input

        [Fact]
        public void Encrypt_LooseMode_AllNonAlphabetic_ReturnsEmpty()
        {
            string input = "123 !@#";
            Assert.Equal("", CaesarCipher.Encrypt(input, 5, CipherMode.Loose));
        }

        // Preserve mode returns input if input contains only non-alphabet members

        [Fact]
        public void Encrypt_PreserveMode_AllNonAlphabetic_RemainsUnchanged()
        {
            string input = "123 !@#";
            Assert.Equal(input, CaesarCipher.Encrypt(input, 5, CipherMode.Preserve));
        }

        // A round trip should return the original input

        [Theory]
        [InlineData("HELLO WORLD", 5, CipherMode.Preserve)]
        [InlineData("ABCXYZ", 3, CipherMode.Loose)]
        public void EncryptThenDecrypt_ReturnsOriginal(string plaintext, int key, CipherMode mode)
        {
            var ciphertext = CaesarCipher.Encrypt(plaintext, key, mode);
            var decrypted = CaesarCipher.Decrypt(ciphertext, key, mode);
            Assert.Equal(
                mode == CipherMode.Loose ? plaintext.Where(c => Defaults.DefaultAlphabet.Contains(char.ToUpper(c))).Aggregate("", (a, b) => a + b)
                                         : plaintext.ToUpper(),
                decrypted);
        }

        // If bruteforce input is an empty string all members of the enumerable must also be empty,
        // aside from the key concatenated at the beginning of each

        [Fact]
        public void BruteForce_EmptyString_ReturnsEmptyForAllKeys()
        {
            var results = CaesarCipher.BruteForce("", CipherMode.Preserve).ToList();
            Assert.All(results, r => Assert.EndsWith("", r));
        }


    }
}