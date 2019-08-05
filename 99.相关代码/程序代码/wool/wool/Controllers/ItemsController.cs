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
    public class ItemsController : Controller
    {
        // GET: Items
        itemBLL bll = new itemBLL();
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
        public ActionResult AddItem()
        {
            return View();
        }
        public ActionResult CheckItem(string objectId)
        {
            return View(new Getitem().GetItem(objectId));
        }
        public ActionResult Edit(string objectId)
        {
            return View(new Getitem().GetItem(objectId));
        }

        public ActionResult Getcomments(int limit = 10, int page = 0, string where = null, string include = null, string order = null)
        {
            CommentBLL bll = new CommentBLL();
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
                    return View(models);
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
                    return View(models);
                    //return ok(new { results = models });
                    }
                }
                catch (Exception e)
                {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("Index", "Items") + "'</script>");
                //return execept(e.Message);
                }
            }

        public ActionResult GetItems(int limit = 10, int page = 0, string where = null, string include = null, string order = null)
        {
            if (Session["CurrentUser"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
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
                    return View("Index", models);
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
                    return View("Index", models);
                    //return ok(new { results = models });
                }
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("Index", "Items") + "'</script>");
                //return execept(e.Message);
            }
        }

        public ActionResult PostItem(item model)
        {
            try
            {
                //表单验证
                if (isNUll(model.title, model.price, model.discount, model.type, model.mall_name, model.url))
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
                model._User = new Getuser().GetUser("627d5312-e6c7-468a-8ccb-cc76e8a28992");
                model.hot = 0;
                model.love = 0;
                model.reward = 0;
                model.commentCount = 0;
                model.state = "未审核";
                bool result = bll.Insert(model);
                if (result)
                {
                    return Content("<script> alert('添加成功'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                    //return create(model);
                }
                return Content("<script> alert('添加失败'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                //return notFound("注册失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("AddItem", "Items") + "'</script>");
                //return execept(e.Message);
            }

        }

        public ActionResult PutItem(item model, string userid)
        {
            try
            {
                model.updatedAt = DateTime.Now;
                model._User = new Getuser().GetUser(userid);
                model.state = "未审核";
                //表单验证
                bool result = bll.Update(model);
                if (result)
                {
                    return Content("<script> alert('修改成功'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                    //return ok(model.updatedAt.ToString("yyyy-mm-dd HH:mm:ss"));
                }
                return Content("<script> alert('修改失败'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                //return notFound("修改失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                //return execept(e.Message);
            }
        }

        public ActionResult DeleteItem(string objectId)
        {
            try
            {
                //表单验证
                if (isNUll(objectId))
                {
                    return Content("<script> alert('参数不能为空'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                    //return invildRequest("参数不能为空");
                }

                bool result = bll.Delete(objectId);
                if (result)
                {
                    return Content("<script> alert('删除成功'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                    //return noContent();
                }
                return Content("<script> alert('删除失败'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                //return notFound("删除失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("GetItems", "Items") + "'</script>");
                //return execept(e.Message);
            }

        }

        public ActionResult DeleteComment(string v1, string objectId)
        {
            CommentBLL bll = new CommentBLL();
            try
            {
                //表单验证
                if (isNUll(objectId))
                {
                    return Content("<script> alert('参数不能为空'); location.href = '" + Url.Action("Getcomments", "Items") + "'</script>");
                    //return invildRequest("参数不能为空");
                }

                bool result = bll.Delete(objectId);
                if (result)
                {
                    return Content("<script> alert('删除成功'); location.href = '" + Url.Action("Getcomments", "Items") + "'</script>");
                    //return ok(new { msg = "ok" });

                }
                return Content("<script> alert('删除失败'); location.href = '" + Url.Action("Getcomments", "Items") + "'</script>");
                //return notFound("删除失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("Getcomments", "Items") + "'</script>");
                //return execept(e.Message);
            }

        }

        public ActionResult PassItem(item model,string userid)
        {
            try
            {
                model.updatedAt = DateTime.Now;
                model._User = new Getuser().GetUser(userid);
                model.state = "1";
                //表单验证
                bool result = bll.Update(model);
                if (result)
                {
                    return Content("<script> alert('审核成功'); location.href = '" + Url.Action("CheckItem", "Items",new { objectId=model.objectId }) + "'</script>");
                    //return ok(model.updatedAt.ToString("yyyy-mm-dd HH:mm:ss"));
                }
                return Content("<script> alert('审核失败'); location.href = '" + Url.Action("CheckItem", "Items",new { objectId = model.objectId }) + "'</script>");
                //return notFound("修改失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("CheckItem", "Items", new { objectId = model.objectId }) + "'</script>");
                //return execept(e.Message);
            }
        }

        public ActionResult Failedpass(item model, string userid)
        {
            try
            {
                model.updatedAt = DateTime.Now;
                model._User = new Getuser().GetUser(userid);
                model.state = "未审核";
                //表单验证
                bool result = bll.Update(model);
                if (result)
                {
                    return Content("<script> alert('审核成功'); location.href = '" + Url.Action("CheckItem", "Items", new { objectId = model.objectId }) + "'</script>");
                    //return ok(model.updatedAt.ToString("yyyy-mm-dd HH:mm:ss"));
                }
                return Content("<script> alert('审核失败'); location.href = '" + Url.Action("CheckItem", "Items", new { objectId = model.objectId }) + "'</script>");
                //return notFound("修改失败");
            }
            catch (Exception e)
            {
                return Content("<script> alert('" + e.Message + "'); location.href = '" + Url.Action("CheckItem", "Items", new { objectId = model.objectId }) + "'</script>");
                //return execept(e.Message);
            }
        }
    }
}