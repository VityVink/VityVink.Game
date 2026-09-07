using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    // ѕарольна€ фраза Ц из неЄ будет сгенерирован ключ и IV
    private const string Password = "MySuperSecretPassword123!";

    public static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            // √енерируем 32-байтный ключ (AES-256) и 16-байтный IV из парол€
            aes.Key = GetKey(Password);
            aes.IV = GetIV(Password);

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encrypted);
        }
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            throw new System.ArgumentException("cipherText is null or empty");

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKey(Password);
            aes.IV = GetIV(Password);
            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
    }

    private static byte[] GetKey(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private static byte[] GetIV(string password)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}