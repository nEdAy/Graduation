using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wool.Models
{
    public class AuthenticationFilter :ActionFilterAttribute 
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var objectId = filterContext.HttpContext.Session["objectId"];
            if (objectId == null)
            {
                //filterContext.Result = new Json(new { code = "0", result = "身份验证失败" }, JsonRequestBehavior.AllowGet);
                //filterContext.Result = new JsonResult
                //{
                //    Data = new { code = "0", result = "身份验证失败" },
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //};
                filterContext.Result = new RedirectResult("/Error/unLogin");
            }
        }

       

    }
}