using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// DES加密类
    /// </summary>
    public sealed class DesSecurity
    {
        private static string key = "cai.deyuan@qq.com";

        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        private DesSecurity()
        {
        }

        public static string Encrypt(string text)
        {
            return DesProvid(text, true);
        }

        public static string Decrypt(string text)
        {
            return DesProvid(text, false);
        }

        private static string DesProvid(string text, bool encrypt)
        {
            string result = "";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = encrypt
                ? Encoding.UTF8.GetBytes(text)
                : Convert.FromBase64String(text);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            ICryptoTransform itf = encrypt
                ? provider.CreateEncryptor(keyBytes, keyIV)
                : provider.CreateDecryptor(keyBytes, keyIV);

            CryptoStream cStream = new CryptoStream(mStream, itf, CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            result = encrypt
                ? Convert.ToBase64String(mStream.ToArray())
                : Encoding.UTF8.GetString(mStream.ToArray());

            mStream.Close();

            return result;
        }
    }
}
