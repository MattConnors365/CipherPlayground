using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using Xunit;

namespace CipherPlayground.Tests.LibraryTests
{
    public class AtbashCipherTests
    {
        // Basic alphabetic input
        [Theory]
        [InlineData("HELLO", "SVOOL")]
        [InlineData("WORLD", "DLIOW")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ZYXWVUTSRQPONMLKJIHGFEDCBA")]
        public void Use_AlphabeticInput_ReturnsReversed(string input, string expected)
        {
            string result = AtbashCipher.Use(input);
            Assert.Equal(expected, result);
        }

        // Non-alphabetic characters handled by mode
        [Theory]
        [InlineData("HELLO!", CipherMode.Loose, "SVOOL")]
        [InlineData("HELLO!", CipherMode.Preserve, "SVOOL!")]
        public void Use_NonAlphabetic_RespectsMode(string input, CipherMode mode, string expected)
        {
            string result = AtbashCipher.Use(input, mode);
            Assert.Equal(expected, result);
        }

        // Strict mode should throw for invalid characters
        [Theory]
        [InlineData("HELLO!")]
        [InlineData("WORLD123")]
        public void Use_StrictMode_InvalidCharacter_Throws(string input)
        {
            Assert.Throws<ArgumentException>(() => AtbashCipher.Use(input, CipherMode.Strict));
        }

        // Case insensitivity
        [Theory]
        [InlineData("hello", "SVOOL")]
        [InlineData("World", "DLIOW")]
        public void Use_MixedCaseInput_NormalizesToUpper(string input, string expected)
        {
            string result = AtbashCipher.Use(input);
            Assert.Equal(expected, result);
        }

        // Empty string returns empty
        [Fact]
        public void Use_EmptyString_ReturnsEmpty()
        {
            string result = AtbashCipher.Use("");
            Assert.Equal("", result);
        }
    }
}
