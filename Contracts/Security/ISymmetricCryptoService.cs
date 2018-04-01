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
using System.Security.Cryptography;

namespace Dqdv.Internal.Contracts.Security
{
    public interface ISymmetricCryptoService
    {
        /// <summary>
        /// Encrypt a string using a key and init vector utils
        /// </summary>
        /// <param name="plainText">Text to Encrypt</param>
        /// <param name="key">32 bytes key</param>
        /// <param name="iv">16 bytes key</param>        
        /// <param name="mode">Selected CipherMode</param>
        /// <param name="paddingMode">Selected PaddingMode</param>
        /// <returns>encrypted string</returns>
        string EncryptString(string plainText, byte[] key, byte[] iv, CipherMode mode, PaddingMode paddingMode);

        /// <summary>
        /// Decrypt a string using a key, init vector, CipherMode and PaddingMode
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="mode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        string DecryptString(string cipherText, byte[] key, byte[] iv, CipherMode mode, PaddingMode paddingMode);
    }
}
