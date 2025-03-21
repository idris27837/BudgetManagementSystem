using BudgetManagementSystem.BusinessLogic.Abstractions;
using System.Security.Cryptography;

#nullable disable
namespace BudgetManagementSystem.BusinessLogic.Concrete
{
    /// <summary>
    /// The managed a e s encryption.
    /// </summary>
    public class ManagedAESEncryption : IManagedAESEncryption
    {
        private readonly AppKey _appKey;
        /// <summary>
        /// Gets or sets the i v.
        /// </summary>
        private byte[] IV { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        private byte[] Key { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedAESEncryption"/> class.
        /// </summary>
        /// <param name="appKey">The app key.</param>
        public ManagedAESEncryption(AppKey appKey)
        {
            _appKey = appKey;
            IV = _appKey.IV;
            Key = _appKey.Key;
        }

        /// <summary>
        /// Encrypts the.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>A Task.</returns>
        public Task<string> Encrypt(string plainText)
        {
            byte[] encrypted;
            // Create a new AesManaged.

            using (AesManaged aes = new())
            {
                // Create encrypto
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);

                // Create MemoryStream
                using MemoryStream ms = new MemoryStream();
                // Create crypto stream using the CryptoStream class. This class is the key to encryption
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                // to encrypt
                using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                // Create StreamWriter and write data to a stream
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);
                encrypted = ms.ToArray();
            }
            // Return encrypted data
            return Task.FromResult(Convert.ToHexString(encrypted));
        }
        /// <summary>
        /// Decrypts the.
        /// </summary>
        /// <param name="plaintext">The plaintext.</param>
        /// <returns>A Task.</returns>
        public Task<string> Decrypt(string plaintext)
        {

            byte[] cipherText = Convert.FromHexString(plaintext);

            // Create AesManaged
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.
                using MemoryStream ms = new MemoryStream(cipherText);
                // Create crypto stream
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                // Read crypto stream
                using StreamReader reader = new StreamReader(cs);
                plaintext = reader.ReadToEnd();
            }
            return Task.FromResult(plaintext);
        }
    }
}
