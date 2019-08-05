using wool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace wool.Controllers
{
    public class baseApiController : ApiController
    {
        public IHttpActionResult retu() {
            return noContent();
        }
        /// <summary>
        /// 验证参数是否为空
        /// </summary>
        /// <param name="checks"></param>
        /// <returns></returns>
        public bool isNUll(params string[] checks)
        {
            foreach (string str in checks) {
                if (string.IsNullOrEmpty(str)) {
                    return true;
                }
            }
            return false;
        }

        #region 返回值
        /// <summary>
        /// 返回json
        /// </summary>
        /// <param name="value"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public CommenResult result(object value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new CommenResult(value, Request, statusCode);
        }
        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public TextResult stringResult(string value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new TextResult(value, Request, statusCode);
        }
        /// <summary>
        /// 正常错误
        /// </summary>
        /// <param name="value"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public CommenResult error(object value, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            return new CommenResult(new { error = value, code = (int)(statusCode) }, Request, statusCode);
        }
        /// <summary>
        /// 200 服务器成功返回用户请求的数据，该操作是幂等的（Idempotent）  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult ok(object value)
        {
            return result(value);
        }
        /// <summary>
        /// 201 [POST/PUT/PATCH]：用户新建或修改数据成功。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult create(object value)
        {
            return result(value, HttpStatusCode.Created);
        }
        /// <summary>
        /// 202 指示返回的元信息来自缓存副本而不是原始服务器，因此可能不正确。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult accepted(object value)
        {
            return result(value, HttpStatusCode.Accepted);
        }
        /// <summary>
        /// 204 [DELETE]：用户删除数据成功。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult noContent()
        {
            return result(new { msg="ok"}, HttpStatusCode.NoContent);
        }
        /// <summary>
        /// 400 INVALID REQUEST - [POST/PUT/PATCH]：用户发出的请求有错误，服务器没有进行新建或修改数据的操作，该操作是幂等的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult invildRequest(object value)
        {
            return error(value, HttpStatusCode.BadRequest);
        }
        /// <summary>
        /// 401 Unauthorized - [*]：表示用户没有权限（令牌、用户名、密码错误）。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult unauthorized(object value)
        {
            return error(value, HttpStatusCode.Unauthorized);
        }
        /// <summary>
        /// 403 Forbidden - [*] 表示用户得到授权（与401错误相对），但是访问是被禁止的。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult forbidden(object value)
        {
            return error(value, HttpStatusCode.Forbidden);
        }
        /// <summary>
        /// 404 NOT FOUND - [*]：用户发出的请求针对的是不存在的记录，服务器没有进行操作，该操作是幂等的。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult notFound(object value)
        {
            return error(value, HttpStatusCode.NotFound);
        }
        /// <summary>
        /// 410 Gone -[GET]：用户请求的资源被永久删除，且不会再得到的。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult gone(object value)
        {
            return error(value, HttpStatusCode.Gone);
        }
        /// <summary>
        /// 500 INTERNAL SERVER ERROR - [*]：服务器发生错误，用户将无法判断发出的请求是否成功。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult serverError(object value)
        {
            return error(value, HttpStatusCode.InternalServerError);
        }
        /// <summary>
        /// 417 execept错误处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CommenResult execept(object value)
        {
            return error(value, HttpStatusCode.ExpectationFailed);
        }

        #endregion
       
	}
}