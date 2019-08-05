using wool.BLL;
using wool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace wool.Models
{
    public class ApiAuthorizationFilter : FilterAttribute, IActionFilter
    {

        public Task<System.Net.Http.HttpResponseMessage> ExecuteActionFilterAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken, Func<Task<System.Net.Http.HttpResponseMessage>> continuation)
        {
            string token = HttpContext.Current.Request.Headers["Session-Token"];

            _UserBLL bll = new _UserBLL();
            var model = bll.QuerySingleByWheres(new List<Wheres> { new Wheres("sessionToken", "=", token) });
            if (model == null)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { error = "未登录", code = (int)HttpStatusCode.Unauthorized });
                });
            }
            HttpContext.Current.Request.Headers.Add("objectId",  model.objectId );
            return continuation();
        }
       
       
    }
}