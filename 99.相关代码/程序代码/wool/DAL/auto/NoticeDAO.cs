using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using wool.Model;
using wool.Utility;
namespace wool.DAO{
public partial class NoticeDAO{
#region 向数据库中添加一条记录 +bool Insert(Notice model)
///<summary>
///向数据库中添加一条记录
///</summary>
///<param name="model">要添加的实体</param>
public bool Insert(Notice model){
const string sql = @"INSERT INTO [dbo].[Notice] (objectId,updatedAt,createdAt,title,url,contents) VALUES (@objectId,@updatedAt,@createdAt,@title,@url,@contents)";
int res = SqlHelper.ExecuteNonQuery(sql,new SqlParameter("@objectId",model.objectId.ToDBValue()),new SqlParameter("@updatedAt",model.updatedAt.ToDBValue()),new SqlParameter("@createdAt",model.createdAt.ToDBValue()),new SqlParameter("@title",model.title.ToDBValue()),new SqlParameter("@url",model.url.ToDBValue()),new SqlParameter("@contents",model.contents.ToDBValue()));
return res >0;
 }
#endregion
 #region 删除一条记录 +bool Delete(string objectId)
/// <summary>
/// 删除一条记录
/// </summary>
/// <param name="objectId">主键</param>
/// <returns>是否成功</returns>
public bool Delete(string objectId)
{
const string sql = "DELETE FROM [dbo].[Notice] WHERE [objectId] = @objectId";
return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@objectId", objectId))>0;
}
#endregion
#region 根据主键ID更新一条记录 +bool Update(Notice model)
/// <summary>
/// 根据主键更新一条记录
/// </summary>
/// <param name="model">更新后的实体</param>
/// <returns>是否成功</returns>
public bool Update(Notice model)
{
const string sql = @"UPDATE [dbo].[Notice] SET  updatedAt=@updatedAt,createdAt=@createdAt,title=@title,url=@url,contents=@contents  WHERE [objectId] = @objectId";
return SqlHelper.ExecuteNonQuery(sql,new SqlParameter("@objectId",model.objectId.ToDBValue()),new SqlParameter("@updatedAt",model.updatedAt.ToDBValue()),new SqlParameter("@createdAt",model.createdAt.ToDBValue()),new SqlParameter("@title",model.title.ToDBValue()),new SqlParameter("@url",model.url.ToDBValue()),new SqlParameter("@contents",model.contents.ToDBValue()))>0;
}
 #endregion
#region 查询条数 +int QueryCount(object wheres)
/// <summary>
/// 查询条数
/// </summary>
/// <param name="wheres">查询条件</param>
/// <returns>条数</returns>
public int QueryCount(object wheres)
{
List<SqlParameter> list=null;
string str = wheres.parseWheres(out list);
str=str==""? str:"where "+str;
string sql = "SELECT COUNT(1) from Notice "+str;var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
return res == null ? 0 : Convert.ToInt32(res);
}
#endregion
#region 查询单个模型实体 +Notice QuerySingleById(string objectId)
/// <summary>
/// 查询单个模型实体
/// </summary>
/// <param name="id">objectId</param>);
/// <returns>实体</returns>);
public Notice QuerySingleById(string objectId)
{
const string sql = "SELECT TOP 1 objectId,updatedAt,createdAt,title,url,contents from Notice WHERE [objectId] = @objectId";
using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@objectId", objectId)))
{
if (reader.HasRows){
reader.Read();
Notice model = SqlHelper.MapEntity<Notice>(reader);
return model;
}
else
{
return null;
}
}
}
#endregion
#region 查询单个模型实体 +_User QuerySingleByIdX(string objectId,string columns){
///<summary>
///查询单个模型实体
///</summary>
///<param name=objectId>主键</param>);
///<returns>实体</returns>);
public Dictionary<string,object> QuerySingleByIdX(string objectId, object columns)
{
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","updatedAt","createdAt","title","url","contents" };
string[] cls = columns.parseColumnsX(clumns,"Notice", out li);
string sql = "SELECT TOP 1 "+string.Join(",", li["Notice"])+" from Notice WHERE [objectId] = @objectId";
using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@objectId", objectId)))
{
if (reader.HasRows)
{
reader.Read();
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);

return model;
}
else
{
return null;
}
}
}
#endregion
#region 查询单个模型实体 +Users QuerySingleByWheres(object wheres)
///<summary>
///查询单个模型实体
///</summary>
///<param name="wheres">条件匿名类</param>
///<returns>实体</returns>
public Notice QuerySingleByWheres(object wheres)
{
var list = QueryList(1, 1, wheres);
return list != null && list.Any() ? list.FirstOrDefault() : null;
}
#endregion
#region 查询单个模型列集合 +Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
///<summary>
///查询单个模型实体
///</summary>
///<param name="wheres">条件</param>
///<param name="columns">列集合</param>
///<returns>实体</returns>
public Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
where = string.IsNullOrEmpty(where) ? "" : " where " + where;
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","updatedAt","createdAt","title","url","contents" };
string[] cls = columns.parseColumnsX(clumns,"Notice", out li);
string sql = "SELECT TOP 1 "+string.Join(",", li["Notice"])+" from Notice"+where;
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
reader.Read();
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);

return model;
}
else
{
return null;
}
}
}
#endregion
#region 分页查询一个集合 +IEnumerable<Users> QueryList(int index, int size, object wheres=null, string orderField=id, bool isDesc = true)
///<summary>
///分页查询一个集合
///</summary>
///<param name="index">页码</param>
///<param name="size">页大小</param>
///<param name="wheres">条件匿名类</param>
///<param name="orderField">排序字段</param>
///<param name="isDesc">是否降序排序</param>
///<returns>实体集合</returns>
public IEnumerable<Notice> QueryList(int index, int size, object wheres=null, string orderField="objectId", bool isDesc = true)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
orderField=string.IsNullOrEmpty(orderField) ? "objectId" : orderField;
var sql = SqlHelper.GenerateQuerySql("Notice",new string[]{"objectId","updatedAt","createdAt","title","url","contents"}, index, size, where, orderField, isDesc);
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
while (reader.Read())
{
Notice model = SqlHelper.MapEntity<Notice>(reader);
yield return model;
}
}
}
}
#endregion
#region 分页查询一个集合 +IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField=id, bool isDesc = true)
///<summary>
///分页查询一个集合
///</summary>
///<param name="index">页码</param>
///<param name="size">页大小</param>
///<param name="columns">指定的列</param>
///<param name="wheres">条件匿名类</param>
///<param name="orderField">排序字段</param>
///<param name="isDesc">是否降序排序</param>
///<returns>实体集合</returns>
public IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField="objectId", bool isDesc = true)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
orderField=string.IsNullOrEmpty(orderField) ? "objectId" : orderField;
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","updatedAt","createdAt","title","url","contents" };
string[] cls = columns.parseColumnsX(clumns,"Notice", out li);
var sql = SqlHelper.GenerateQuerySql("Notice", li["Notice"], index, size, where, orderField, isDesc);
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
while (reader.Read())
{
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);

yield return model;
}
}
}
}
#endregion
#region 根据主键修改指定列 +bool UpdateById(string objectId,object columns)
/// <summary>
/// 根据主键更新指定记录
/// </summary>
/// <param name="objectId">主键</param>
/// <param name="columns">列集合对象</param>
/// <returns>是否成功</returns>
public bool UpdateById(string objectId, object columns)
{
List<SqlParameter> list = null;
string[] column = columns.parseColumns(out list);
list.Add(new SqlParameter("@objectId", objectId.ToDBValue()));
string sql = string.Format(@"UPDATE [dbo].[Notice] SET  {0}  WHERE [{1}] = @{1}", string.Join(",", column), "objectId");
return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
}
 #endregion
#region 根据条件更新记录+bool UpdateByWheres(object wheres, object columns)
/// <summary>
/// 根据条件更新记录
/// </summary>
/// <param name="wheres">条件集合实体实体</param>
/// <param name="columns">列集合对象</param>
/// <returns>是否成功</returns>
public bool UpdateByWheres(object wheres, object columns)
{
List<SqlParameter> list = null;
string[] column = columns.parseColumns(out list);
List<SqlParameter> list1 = null;
string where = wheres.parseWheres(out list1);
where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
list.AddRange(list1);
string sql = string.Format(@"UPDATE [dbo].[Notice] SET  {0} {1}", string.Join(",", column), where);
return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
}
 #endregion
}
}