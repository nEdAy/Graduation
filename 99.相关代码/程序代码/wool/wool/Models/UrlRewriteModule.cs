using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace wool.Models
{
    /// <summary>
    /// 必须要在Web.config文件中配置该模块
    /// </summary>
    public class UrlRewriteModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            //context.BeginRequest += (s, e) =>
            //{
            //    HttpApplication app = s as HttpApplication;
            //    // ~/list-3-4-21.aspx  ~/index.aspx
            //    var exePath = app.Context.Request.AppRelativeCurrentExecutionFilePath;
            //    // target:/list.aspx?page=3&sort=4&cat=21
            //    Regex regex = new Regex(@"~/list-(\d+)-(\d+)-(\d+).aspx");
            //    if (regex.IsMatch(exePath))
            //    {
            //        // 当前是伪静态格式

            //        // 翻译成真实路径
            //        var realPath = regex.Replace(exePath, @"~/list.aspx?cat=$1&sort=$2&page=$3");

            //        //Response.Redirect(realPath);
            //        //Server.Transfer(realPath);
            //        // 重写当前请求地址
            //        app.Context.RewritePath(realPath);
            //    }
            //};
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //去除头里的 server属性
            HttpContext.Current.Response.Headers.Remove("Server");
        }

       
        public void Dispose()
        {

        }
    }
}