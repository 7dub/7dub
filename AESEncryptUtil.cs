using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.daervin.common.utils
{
    public class AESEncryptUtil
    {
        private static String KEY = "7dub@utilAESDEnc";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="data">原始串</param>
        /// <returns>加密后的串</returns>
        public static string aesEncrypt(String data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(data);
            Byte[] keyBytes = new Byte[16];
            Array.Copy(Encoding.ASCII.GetBytes(KEY), keyBytes,16);
            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged()           
            {
                Key = keyBytes,
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return byteToHexStr(resultArray);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="data">加密后的串</param>
        /// <returns>原始串</returns>
        public static string aesDecrypt(String data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            byte[] content = hex2Bytes(data);
            Byte[] keyBytes = new Byte[16];
            Array.Copy(Encoding.ASCII.GetBytes(KEY), keyBytes, 16);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = keyBytes,
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(content, 0, content.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>16进制字符串</returns>
        private static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("x2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexStr">16进制字符串</param>
        /// <returns>字节数组</returns>
        private static byte[] hex2Bytes(String hexStr)
        {
            hexStr = hexStr.Replace(" ", "");
            if ((hexStr.Length % 2) != 0)
                hexStr += " ";
            byte[] returnBytes = new byte[hexStr.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        internal static string aesEncrypt(long p)
        {
            throw new NotImplementedException();
        }

        internal static string aesEncrypt(Func<string> func)
        {
            throw new NotImplementedException();
        }

     
    }
}
