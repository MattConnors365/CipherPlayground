using System.Text;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.Tests.LibraryTests
{
    public class CommonTests
    {
        [Theory]
        [InlineData('!', CipherMode.Strict)]
        [InlineData('1', CipherMode.Strict)]
        public void HandleNonAlphabetic_Strict_Throws(char c, CipherMode mode)
        {
            var sb = new StringBuilder();
            Assert.Throws<ArgumentException>(() => HandleNonAlphabetic(c, mode, sb));
        }

        [Theory]
        [InlineData('!', CipherMode.Loose)]
        [InlineData('1', CipherMode.Loose)]
        public void HandleNonAlphabetic_Loose_Skips(char c, CipherMode mode)
        {
            var sb = new StringBuilder();
            HandleNonAlphabetic(c, mode, sb);
            Assert.Equal("", sb.ToString());
        }

        [Theory]
        [InlineData('!', CipherMode.Preserve)]
        [InlineData('1', CipherMode.Preserve)]
        public void HandleNonAlphabetic_Preserve_Appends(char c, CipherMode mode)
        {
            var sb = new StringBuilder();
            HandleNonAlphabetic(c, mode, sb);
            Assert.Equal(c.ToString(), sb.ToString());
        }

        [Theory]
        [InlineData("INVALID", CipherMode.Strict)]
        public void HandleInvalidToken_Strict_Throws(string token, CipherMode mode)
        {
            var sb = new StringBuilder();
            Assert.Throws<FormatException>(() => HandleInvalidToken(token, mode, sb));
        }

        [Theory]
        [InlineData("SKIPME", CipherMode.Loose)]
        public void HandleInvalidToken_Loose_Skips(string token, CipherMode mode)
        {
            var sb = new StringBuilder();
            HandleInvalidToken(token, mode, sb);
            Assert.Equal("", sb.ToString());
        }

        [Theory]
        [InlineData("PRESERVE", CipherMode.Preserve)]
        public void HandleInvalidToken_Preserve_Appends(string token, CipherMode mode)
        {
            var sb = new StringBuilder();
            HandleInvalidToken(token, mode, sb);
            Assert.Equal(token, sb.ToString());
        }

        [Fact]
        public void Defaults_ContainExpectedValues()
        {
            Assert.Equal(26, Defaults.DefaultAlphabet.Length);
            Assert.Equal("HELLO", Defaults.DefaultPlaintext);
            Assert.Equal(CipherMode.Preserve, Defaults.DefaultMode);
        }
    }
}
