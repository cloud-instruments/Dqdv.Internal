/*
Copyright(c) <2018> <University of Washington>
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Dqdv.Internal.Contracts.Security;

namespace Dqdv.Internal.Security
{
    public class RijndaelCryptoService : ISymmetricCryptoService
    {
        ////////////////////////////////////////////////////////////
        // Constants, Enums and Class members
        ////////////////////////////////////////////////////////////

        /// <summary>
        /// Default key
        /// </summary>
        public static readonly byte[] DefaultKey =
        {
            117
        };

        /// <summary>
        /// Default IV
        /// </summary>
        public static readonly byte[] DefaultIV =
        {
            212
        };

        /// <summary>
        /// Default cipher mode
        /// </summary>
        public const CipherMode DefaultCipherMode = CipherMode.CBC;

        /// <summary>
        /// Default padding mode
        /// </summary>
        public const PaddingMode DefaultPaddingMode = PaddingMode.PKCS7;


        ////////////////////////////////////////////////////////////
        // Public Methods/Atributes
        ////////////////////////////////////////////////////////////

        /// <inheritdoc />
        /// <summary>
        /// Encrypt a string using a key and init vector utils
        /// </summary>
        /// <param name="plainText">Text to Encrypt</param>
        /// <param name="key">32 bytes key</param>
        /// <param name="iv">16 bytes key</param>        
        /// <param name="mode">Selected CipherMode</param>
        /// <param name="paddingMode">Selected PaddingMode</param>
        /// <returns>encrypted string</returns>
        public string EncryptString(string plainText, byte[] key, byte[] iv, CipherMode mode, PaddingMode paddingMode)
        {
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var aesAlg = new RijndaelManaged { Mode = mode, Padding = paddingMode, Key = key, IV = iv})
            {
                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption.
                var memoryStream = new MemoryStream();
                using (var stream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        //Write all data to the stream.
                        writer.Write(plainText);
                    }

                    // Return the encrypted bytes from the memory stream.
                    return Base32.ToString(memoryStream.ToArray());
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Decrypt a string using a key, init vector, CipherMode and PaddingMode
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="mode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public string DecryptString(string cipherText, byte[] key, byte[] iv, CipherMode mode, PaddingMode paddingMode)
        {
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // hold the decrypted text.
            string resultText;

            try
            {
                aesAlg = new RijndaelManaged { Mode = mode, Padding = paddingMode, Key = key, IV = iv };

                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(Base32.ToBytes(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            resultText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                aesAlg?.Clear();
            }

            return resultText;
        }
    }
}
