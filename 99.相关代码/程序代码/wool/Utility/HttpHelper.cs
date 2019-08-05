// ***********************************************************************
// Project			: Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2013-11-18 14:31
//
// Last Modified By : iceStone
// Last Modified On : 2013-11-18 14:34
// ***********************************************************************
// <copyright file="HttpHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wool.Utility
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;

    /// <summary>
    /// HTTP请求操作助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    /// </remarks>
    public static class HttpHelper
    {
        #region 以GET方式抓取远程页面内容 +static string Get(string url, object data)
        /// <summary>
        /// 以GET方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="data">参数列表</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Get("http://www.wedn.net",new{p1="hello",p2="world"});
        ///   </example>
        public static string Get(string url, object data)
        {
            var queryString = string.Join("&",
                                from p in data.GetType().GetProperties()
                                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(data, null).ToString()));
            return Get(url, queryString, Encoding.UTF8);
        }
        #endregion

        #region 以GET方式抓取远程页面内容 +static string Get(string url, object data)
        /// <summary>
        /// 以GET方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="data">参数列表</param>
        /// <param name="encoding"></param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Get("http://www.wedn.net",new{p1="hello",p2="world"});
        /// </example>
        public static string Get(string url, object data, Encoding encoding)
        {
            var queryString = string.Join("&",
                                from p in data.GetType().GetProperties()
                                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(data, null).ToString()));
            return Get(url, queryString, encoding);
        }
        #endregion

        #region 以GET方式抓取远程页面内容 +static string Get(string url, string data)
        /// <summary>
        /// 以GET方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="queryString">参数列表</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Get("http://www.wedn.net","p1=hello& p2=world");
        /// </example>
        public static string Get(string url, string queryString)
        {
            return Get(url, queryString, Encoding.UTF8);
        }
        #endregion

        #region 以GET方式抓取远程页面内容 +static string Get(string url, string data, Encoding encoding)
        /// <summary>
        /// 以GET方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="queryString">参数列表</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Get("http://www.wedn.net","p1=hello& p2=world");
        ///   </example>
        public static string Get(string url, string queryString, Encoding encoding)
        {
            string res = string.Empty;
            try
            {
                //if (data.Contains("{"))
                //    data = data.TrimStart('{').TrimEnd('}').Replace(":", "=").Replace(",", "&").Replace(" ", string.Empty);
                var request = WebRequest.Create(string.Format("{0}?{1}", url, queryString)) as HttpWebRequest;
                if (request == null) return string.Empty;
                request.Timeout = 19600;
                //bmob支付   其他项目删除
                request.Headers.Add("X-Bmob-Application-Id", "417ad80af2de7cc79d976ed980d308a0");
                request.Headers.Add("X-Bmob-REST-API-Key", "ce5bf94363345371af655621a8d57c41");

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
                var response = (HttpWebResponse)request.GetResponse();
               
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var reader = new StreamReader(stream, encoding);
                        var sb = new StringBuilder();
                        while (-1 != reader.Peek())
                        {
                            sb.Append(reader.ReadLine() + "\r\n");
                        }
                        res = sb.ToString();
                    }
                }
                response.Close();
            }
            catch (Exception ee)
            {
                res = ee.Message;
            }
            return res;
        }
        #endregion

        #region 以POST方式抓取远程页面内容 +static string Post(string url, object data)
        /// <summary>
        /// 以POST方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="data">参数列表</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Post("http://www.wedn.net",new{p1="hello",p2="world"});
        /// </example>
        public static string Post(string url, object data)
        {
            return Post(url, data, Encoding.UTF8);
        }
        #endregion

        #region 以POST方式抓取远程页面内容 +static string Post(string url, object data, Encoding encoding)
        /// <summary>
        /// 以POST方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="data">参数列表</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Post("http://www.wedn.net",new{p1="hello",p2="world"});
        /// </example>
        public static string Post(string url, object data, Encoding encoding)
        {
            var queryString = string.Join("&",
                                from p in data.GetType().GetProperties()
                                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(data, null).ToString()));
            return Post(url, queryString, encoding);
        }
        #endregion

        #region 以POST方式抓取远程页面内容 +static string Post(string url, string querySting)
        /// <summary>
        /// 以POST方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="querySting">参数列表</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Post("http://www.wedn.net","p1=hello& p2=world");
        ///   </example>
        public static string Post(string url, string querySting)
        {
            return Post(url, querySting, Encoding.UTF8);
        }
        #endregion

        #region 以POST方式抓取远程页面内容 +static string Post(string url, string querySting, Encoding encoding)
        /// <summary>
        /// 以POST方式抓取远程页面内容
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">请求URL地址</param>
        /// <param name="querySting">参数列表</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>响应资源体</returns>
        /// <example>
        /// string str=HtmlHelper.Post("http://www.wedn.net","p1=hello& p2=world",Encoding.UTF8);
        ///   </example>
        public static string Post(string url, string querySting, Encoding encoding)
        {
            string strResult = string.Empty;
            try
            {
                //if (querySting.Contains("{"))
                //    querySting = querySting.TrimStart('{').TrimEnd('}').Replace(":", "=").Replace(",", "&").Replace(" ", string.Empty);
                var postData = encoding.GetBytes(querySting);
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request == null) return string.Empty;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(postData, 0, postData.Length); //设置POST
                newStream.Close();
                var response = request.GetResponse() as HttpWebResponse;
                if (response == null) return string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var reader = new StreamReader(stream, encoding);
                        strResult = reader.ReadToEnd();
                    } 
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }
        #endregion

        #region 获取远程数据流 +static Stream GetStream(string url, object data)
        /// <summary>
        /// 获取远程数据流
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">地址</param>
        /// <param name="data">参数列表</param>
        /// <returns>数据流</returns>
        /// <example>
        /// Stream s = HttpHelper.GetStream("http://www.wedn.net", "aid=15000102& b=43878429697395826");
        /// picVerify.Image = Image.FromStream(s);
        ///   </example>
        public static Stream GetStream(string url, object data)
        {
            var queryString = string.Join("&",
                               from p in data.GetType().GetProperties()
                               select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(data, null).ToString()));
            return GetStream(url, queryString);
        }
        #endregion

        #region 获取远程数据流 +static Stream GetStream(string url, string queryString)
        /// <summary>
        /// 获取远程数据流
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:56 Created By iceStone
        /// </remarks>
        /// <param name="url">地址</param>
        /// <param name="queryString">参数列表</param>
        /// <returns>数据流</returns>
        /// <example>
        /// Stream s = HttpHelper.GetStream("http://www.wedn.net", "aid=15000102& b=43878429697395826");
        /// picVerify.Image = Image.FromStream(s);
        ///   </example>
        public static Stream GetStream(string url, string queryString)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                //if (data.Contains("{"))
                //    data = data.TrimStart('{').TrimEnd('}').Replace(":", "=").Replace(",", "&").Replace(" ", string.Empty);
                request = WebRequest.Create(string.Format("{0}?{1}", url, queryString)) as HttpWebRequest;
                if (request == null) return null;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ServicePoint.ConnectionLimit = 300;
                request.Referer = url;
                request.Accept =
                    "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
                request.UserAgent =
                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                request.Method = "GET";
                response = request.GetResponse() as HttpWebResponse;
                return response == null ? null : response.GetResponseStream();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (request != null)
                    request.Abort();

                if (response != null)
                    response.Close();
            }
        }
        #endregion
    }
}
