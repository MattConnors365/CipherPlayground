namespace CipherPlayground.Library
{
    public class CaesarCipher
    {
        public static string Encrypt(string plaintext, int key, CipherMode mode = CipherMode.Strict)
        {
            char[] alphabet = ['A', 'B', 'C'];
        }
        public static string Decrypt(string plaintext, int key)
        {
            return Encrypt(plaintext, key)
        }
    }
}
