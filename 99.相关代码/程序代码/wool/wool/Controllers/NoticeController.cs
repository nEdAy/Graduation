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
    public class NoticeController : Controller
    {

        NoticeBLL bll = new NoticeBLL();
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
        public ActionResult GetActivities(string v1, int limit = 10, int page = 0, string where = null, string include = null, string order = null)
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
                    return View(models);
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
                    return View(models);
                }
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("Index", "Items") + "'</script>");
            }

        }
        public ActionResult addnotice() {
            return View("addNotice");
        }
        public ActionResult PostNotice(Notice model)
        {
            NoticeBLL bll= new NoticeBLL();
            try
            {
                //表单验证
                if (isNUll(model.title, model.contents ,model.url))
                {
                    return Content("<script> alert('参数不能为空'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                    //return invildRequest("参数不能为空");
                }
                //主键
                Guid guid = Guid.NewGuid();
                model.objectId = guid.ToString();
                //时间
                model.createdAt = DateTime.Now;
                model.updatedAt = DateTime.Now;
            
                bool result = bll.Insert(model);
                if (result)
                {
                    return Content("<script> alert('发布成功'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                    //return create(model);
                }
                return Content("<script> alert('发布失败'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                //return notFound("注册失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                //return execept(e.Message);
            }

        }
    }
}
    
