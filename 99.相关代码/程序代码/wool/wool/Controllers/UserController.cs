using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using wool;
using wool.BLL;
using wool.Model;
using wool.Utility;
namespace wool.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        _UserBLL bll = new _UserBLL();
        public bool isNUll(params string[] checks)
        {
          
            foreach (string str in checks)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return true;
                }
            }
            return false;
        }
 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUser(int limit = 10, int page = 0, string where = null, string include = null, string order = null)
        {
            try
            {
                List<Wheres> list = new List<Wheres>();
                Boolean isDesc = true;
                string orderField = "updatedAt";
                //条件
                if (!string.IsNullOrEmpty(where))
                {
                    list = JsonHelper.Deserialize<List<Wheres>>(where);
                }
                //判断页码
                //if (limit == 0)
                //{
                //    int num = bll.QueryCount(list);
                //    return ok(new { results = new string[] { }, count = num });
                //}
                //排序
                if (!string.IsNullOrEmpty(order))
                {
                    string descStr = order.Substring(0, 1);
                    if (isDesc.Equals("-"))
                    {
                        isDesc = true;
                    }
                    else
                    {
                        isDesc = false;
                    }

                    orderField = order.Substring(1);
                }
                //列集合
                if (string.IsNullOrEmpty(include))
                {
                    //为空时，查询所有列
                    var models = bll.QueryList(page, limit, list, orderField, isDesc);
                    return View("DisplayUesr", models);
                    //return ok(new { results = models });
                }
                else
                {
                    //非空时，解析所有列
                    Dictionary<string, string[]> columns = new Dictionary<string, string[]>();
                    string includeInit = include.Substring(0, include.Count() - 1);
                    string[] cols = includeInit.Split(new string[] { "]," }, StringSplitOptions.None);
                    foreach (var col in cols)
                    {
                        string[] cols1 = col.Split('[');
                        columns.Add(cols1[0], cols1[1].Split('|'));
                    }
                    var models = bll.QueryListX(page, limit, columns, list, orderField, isDesc);
                    return View("DisplayUesr",models);
                    //return ok(new { results = models });
                }
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("Index", "Items") + "'</script>");
                //return execept(e.Message);
            }
          
        }

    }
}