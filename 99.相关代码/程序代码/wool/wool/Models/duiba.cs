using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace wool.Models
{
    public class duiba
    {
        /**
                 * 构建一个带兑吧签名的url
                 * @param url 接口url地址
                 * @param hshTable 需要传输的参数,不要包含appKey和appSecret
                 * @param appKey 
                 * @param appSecret 私钥
                 * @return
                 */
        public static string BuildUrlWithSign(string url, Hashtable hshTable, string appKey, string appSecret)
        {
            hshTable.Add("appKey", appKey);
            hshTable.Add("appSecret", appSecret);
            if (!hshTable.ContainsKey("timestamp"))
            {
                hshTable.Add("timestamp", GetTimeStamp());
            }
            string sign = Sign(hshTable);
            hshTable.Add("sign", sign);
            //此处删除appSecret，因为appSecret不能出现在url上
            hshTable.Remove("appSecret");
            return AssembleUrl(url, hshTable);

        }

        /**
         * 拼接Url参数
         * @param url 接口地址
         * @param hshTable 参数
         * return
         */
        public static string AssembleUrl(string url, Hashtable hshTable)
        {
            if (url.IndexOf("?") < 0)
            {
                url += "?";
            }
            foreach (DictionaryEntry de in hshTable)
            {
                url += de.Key.ToString() + "=" + UrlUtf8DeCode(de.Value.ToString()) + "&";
            }
            return url;
        }

        /**
         * 解析出URL地址中的请求参数
         * @param request HttpRequest对象 
         * return
         * 特别注意！在兑吧产生的URL中通常存在中文，从而会进行URL编码。
         * 所以在解析request参数之前要进行URL解码，要不然可能造成签名验证失败。
         * 参考代码：Server.UrlDecode(Request.Url.ToString())
         */
        public static Hashtable GetUrlParams(HttpRequest request)
        {
            Hashtable param = new Hashtable();
            string[] urlInfo = request.Url.ToString().Split('?');
            if (urlInfo.Length > 1)
            {
                string[] paramInfo = urlInfo[1].Split('&');
                foreach (string s in paramInfo)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        param.Add(s.Split('=')[0], s.Split('=')[1]);
                    }
                }
            }
            return param;
        }

        /**
        * 签名函数，内部包含对hshTable进行排序后的一系列处理
        * @param hshTable 哈希表
        * return 
        */
        public static string Sign(Hashtable hshTable)
        {
            string key = string.Empty;
            if (hshTable.ContainsKey("sign"))
            {
                hshTable.Remove("sign");
            }
            ArrayList akeys = new ArrayList(hshTable.Keys); //记得导入System.Collections  
            akeys.Sort(); //调用了akeys的按字母顺序进行排序Sort,这个很容易单独实现  
            //akeys.Reverse();
            foreach (string skey in akeys)
            {
                key += hshTable[skey];
            }
            string sign = GetMd5(key);//用函数对key进行MD5签名
            return sign.ToLower();
        }

        /**
        * 签名验证，放入参数hshTable和密钥
        * @param appSecret
        * @param hshTable 其中必须包含sign参数
        * @return
        */
        public static bool SignVerify(string appSecret, Hashtable hshTable)
        {

            string sign = string.Empty;
            hshTable.Add("appSecret", appSecret);
            if (hshTable.ContainsKey("sign"))
            {
                sign = hshTable["sign"].ToString().ToLower();
            }
            if (sign == Sign(hshTable))
                return true;
            else
                return false;

        }


        /// 获取时间戳  

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        //对参数进行utf-8统一编码
        public static string UrlUtf8DeCode(string str)
        {
            Encoding utf8 = Encoding.UTF8;
            return HttpUtility.UrlDecode(str, utf8);
        }

        public static string GetMd5(string original)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(original));

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }
    }
}