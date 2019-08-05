// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncryptExtension.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   加密拓展方法类
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 加密拓展方法类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:53 Created By iceStone
    /// </remarks>
    public static class EncryptHelper
    {
        #region MD5加密 +static string Md5(this string str)
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5(this string str)
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sbd = new StringBuilder();
            foreach (var item in bytes)
            {
                sbd.Append(item.ToString("x2"));
            }
            return sbd.ToString();
        }
        #endregion

        #region 基于MD5的自定义加密字符串方法（非MD5） +static string Encrypt(this string str, string key = "iceStone")
        /// <summary>
        /// 基于MD5的自定义加密字符串方法(不可逆)（非MD5）
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(this string str, string key = "iceStone")
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var eKey = new StringBuilder();
            foreach (var item in bytes)
            {
                eKey.Append(item.ToString("x"));
            }

            // 字符串散列值+密钥再次计算散列值
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key + eKey));
            var pwd = new StringBuilder();
            foreach (var item in bytes)
            {
                pwd.Append(item.ToString("x"));
            }

            return pwd.ToString();
        }
        #endregion

        #region 短加密 +static string ShortEncrypt(this string url, string key = "iceStone")
        /// <summary>
        /// 短加密
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ShortEncrypt(this string url, string key = "iceStone")
        {
            ////要使用生成URL的字符
            // var chars = new[]{
            // "a","b","c","d","e","f","g","h",
            // "i","j","k","l","m","n","o","p",
            // "q","r","s","t","u","v","w","x",
            // "y","z","0","1","2","3","4","5",
            // "6","7","8","9","A","B","C","D",
            // "E","F","G","H","I","J","K","L",
            // "M","N","O","P","Q","R","S","T",
            // "U","V","W","X","Y","Z"

            // };
            ////对传入网址进行MD5加密
            // string hex = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

            // string[] resUrl = new string[4];

            // for (int i = 0; i < 4; i++)
            // {
            // //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
            // int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
            // string outChars = string.Empty;
            // for (int j = 0; j < 6; j++)
            // {
            // //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
            // int index = 0x0000003D & hexint;
            // //把取得的字符相加
            // outChars += chars[index];
            // //每次循环按位右移5位
            // hexint = hexint >> 5;
            // }
            // //把字符串存入对应索引的输出数组
            // resUrl[i] = outChars;
            // }

            // return resUrl;
            return string.Empty;
        }
        #endregion

        #region 加密一个字符串 +static string EncryptStr(this string str, string key = "iceStone")
        /// <summary>
        /// 加密一个字符串(可逆，非固定)
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptStr(this string str, string key = "iceStone")
        {
            var des = DES.Create();

            // var timestamp = DateTime.Now.ToString("HHmmssfff");
            var inputBytes = Encoding.UTF8.GetBytes(MixUp(str));
            var keyBytes = Encoding.UTF8.GetBytes(key);
            SHA1 ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyBytes);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    var ret = new StringBuilder();
                    foreach (var b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }

                    return ret.ToString();
                }
            }
        }
        #endregion

        #region 解密一个字符串 +static string DecryptStr(this string str, string key = "iceStone")
        /// <summary>
        /// 解密一个字符串
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptStr(this string str, string key = "iceStone")
        {
            var des = DES.Create();
            var inputBytes = new byte[str.Length / 2];
            for (var x = 0; x < str.Length / 2; x++)
            {
                inputBytes[x] = (byte)System.Convert.ToInt32(str.Substring(x * 2, 2), 16);
            }
            var keyByteArray = Encoding.UTF8.GetBytes(key);
            var ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyByteArray);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    return ClearUp(Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
        }
        #endregion

        #region 混淆与反混淆

        // private const string TimestampFormat = "ddHHmmssfff";

        /// <summary>
        /// The timestamp length.
        /// </summary>
        private const int TimestampLength = 36;

        /// <summary>
        /// 用时间简单混淆
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>混淆后字符串</returns>
        public static string MixUp(string str)
        {
            // var timestamp = DateTime.Now.ToString(TimestampFormat);
            var timestamp = Guid.NewGuid().ToString();
            var count = str.Length + TimestampLength;
            var sbd = new StringBuilder(count);
            int j = 0;
            int k = 0;
            for (int i = 0; i < count; i++)
            {
                if (j < TimestampLength && k < str.Length)
                {
                    if (i % 2 == 0)
                    {
                        sbd.Append(str[k]);
                        k++;
                    }
                    else
                    {
                        sbd.Append(timestamp[j]);
                        j++;
                    }
                }
                else if (j >= TimestampLength)
                {
                    sbd.Append(str[k]);
                    k++;
                }
                else if (k >= str.Length)
                {
                    break;

                    // sbd.Append(timestamp[j]);
                    // j++;
                }
            }

            return sbd.ToString();
        }

        /// <summary>
        /// 简单反混淆
        /// </summary>
        /// <param name="str">混淆后字符串</param>
        /// <returns>原来字符串</returns>
        public static string ClearUp(string str)
        {
            var sbd = new StringBuilder();
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sbd.Append(str[i]);
                }
                else
                {
                    j++;
                }

                if (j > TimestampLength)
                {
                    sbd.Append(str.Substring(i));
                    break;
                }
            }

            return sbd.ToString();
        }

        #endregion
    }
}