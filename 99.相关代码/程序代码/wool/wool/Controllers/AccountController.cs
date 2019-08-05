using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wool.BLL;
using wool.Model;

namespace wool.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        _UserBLL bll = new _UserBLL();
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult LogIn()
        {
            string username = Request.Form["username"];//获取用户名
            string password = Request.Form["password"];//获取密码
              try
            {
                //表单验证
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                { 
                    return Content("<script> alert('用户名密码不得为空'); location.href = '" + Url.Action("Index", "Account") + "'</script>");
                }
                else
                {
                  if (username != "admin")
                   {
                       return Content("<script> alert('该用户没有权限登录'); location.href = '" + Url.Action("Index", "Account") + "'</script>");
                   }
                    List<Wheres> whs = new List<Wheres>() { new Wheres("username", "=", username) };
                    var dir = bll.QuerySingleByWheres(whs);
                    if (dir != null)
                    {
                        string obj = (string)(dir.objectId);
                        string pas = (string)(dir.password);
                        //string li = "raw:" + password + "  sql:" + pas + "  jiami:" + (password + obj).Md5();
                        //string ss = (password + obj).Md5();
                        if ((password.Md5() + obj).Md5().Equals(pas))
                        {
                            string sessionToken = Guid.NewGuid().ToString();
                            bll.UpdateById(obj, new Dictionary<string, object> { { "sessionToken", sessionToken } });

                            _User model = bll.QuerySingleById(obj);
                            Session["CurrentUser"] = username;
                            return RedirectToAction("GetItems", "Items");
                            //return ok(model);
                        }
                        else
                        {
                            return Content("<script> alert('密码错误'); location.href = '" + Url.Action("Index", "Account") + "'</script>");
                            // return notFound("密码错误" + li);
                        }
                    }
                    else
                    {
                        return Content("<script> alert('用户不存在'); location.href = '" + Url.Action("Index", "Account") + "'</script>");
                        //return notFound("用户不存在");
                    }
                }
            }
            catch (Exception e)
            {
                return Content("<script> alert('"+ e.Message+"'); location.href = '" + Url.Action("Index", "Account") + "'</script>");
                //return execept(e.Message);
            }
        }
    }
}