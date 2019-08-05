using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using wool.Model;
using wool.Utility;
namespace wool.DAO{
public partial class itemDAO{
#region �����ݿ������һ����¼ +bool Insert(item model)
///<summary>
///�����ݿ������һ����¼
///</summary>
///<param name="model">Ҫ��ӵ�ʵ��</param>
public bool Insert(item model){
const string sql = @"INSERT INTO [dbo].[item] (objectId,_UserId,title,price,discount,details,type,mall_name,label,hot,love,reward,pic_a,pic_b,pic_c,url,commentCount,state,admin,updatedAt,createdAt) VALUES (@objectId,@_UserId,@title,@price,@discount,@details,@type,@mall_name,@label,@hot,@love,@reward,@pic_a,@pic_b,@pic_c,@url,@commentCount,@state,@admin,@updatedAt,@createdAt)";
int res = SqlHelper.ExecuteNonQuery(sql,new SqlParameter("@objectId",model.objectId.ToDBValue()),new SqlParameter("@_UserId",model._User.objectId.ToDBValue()),new SqlParameter("@title",model.title.ToDBValue()),new SqlParameter("@price",model.price.ToDBValue()),new SqlParameter("@discount",model.discount.ToDBValue()),new SqlParameter("@details",model.details.ToDBValue()),new SqlParameter("@type",model.type.ToDBValue()),new SqlParameter("@mall_name",model.mall_name.ToDBValue()),new SqlParameter("@label",model.label.ToDBValue()),new SqlParameter("@hot",model.hot.ToDBValue()),new SqlParameter("@love",model.love.ToDBValue()),new SqlParameter("@reward",model.reward.ToDBValue()),new SqlParameter("@pic_a",model.pic_a.ToDBValue()),new SqlParameter("@pic_b",model.pic_b.ToDBValue()),new SqlParameter("@pic_c",model.pic_c.ToDBValue()),new SqlParameter("@url",model.url.ToDBValue()),new SqlParameter("@commentCount",model.commentCount.ToDBValue()),new SqlParameter("@state",model.state.ToDBValue()),new SqlParameter("@admin",model.admin.ToDBValue()),new SqlParameter("@updatedAt",model.updatedAt.ToDBValue()),new SqlParameter("@createdAt",model.createdAt.ToDBValue()));
return res >0;
 }
#endregion
 #region ɾ��һ����¼ +bool Delete(string objectId)
/// <summary>
/// ɾ��һ����¼
/// </summary>
/// <param name="objectId">����</param>
/// <returns>�Ƿ�ɹ�</returns>
public bool Delete(string objectId)
{
const string sql = "DELETE FROM [dbo].[item] WHERE [objectId] = @objectId";
return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@objectId", objectId))>0;
}
#endregion
#region ��������ID����һ����¼ +bool Update(item model)
/// <summary>
/// ������������һ����¼
/// </summary>
/// <param name="model">���º��ʵ��</param>
/// <returns>�Ƿ�ɹ�</returns>
public bool Update(item model)
{
const string sql = @"UPDATE [dbo].[item] SET  _UserId=@_UserId,title=@title,price=@price,discount=@discount,details=@details,type=@type,mall_name=@mall_name,label=@label,hot=@hot,love=@love,reward=@reward,pic_a=@pic_a,pic_b=@pic_b,pic_c=@pic_c,url=@url,commentCount=@commentCount,state=@state,admin=@admin,updatedAt=@updatedAt,createdAt=@createdAt  WHERE [objectId] = @objectId";
return SqlHelper.ExecuteNonQuery(sql,new SqlParameter("@objectId",model.objectId.ToDBValue()),new SqlParameter("@_UserId",model._User.objectId.ToDBValue()),new SqlParameter("@title",model.title.ToDBValue()),new SqlParameter("@price",model.price.ToDBValue()),new SqlParameter("@discount",model.discount.ToDBValue()),new SqlParameter("@details",model.details.ToDBValue()),new SqlParameter("@type",model.type.ToDBValue()),new SqlParameter("@mall_name",model.mall_name.ToDBValue()),new SqlParameter("@label",model.label.ToDBValue()),new SqlParameter("@hot",model.hot.ToDBValue()),new SqlParameter("@love",model.love.ToDBValue()),new SqlParameter("@reward",model.reward.ToDBValue()),new SqlParameter("@pic_a",model.pic_a.ToDBValue()),new SqlParameter("@pic_b",model.pic_b.ToDBValue()),new SqlParameter("@pic_c",model.pic_c.ToDBValue()),new SqlParameter("@url",model.url.ToDBValue()),new SqlParameter("@commentCount",model.commentCount.ToDBValue()),new SqlParameter("@state",model.state.ToDBValue()),new SqlParameter("@admin",model.admin.ToDBValue()),new SqlParameter("@updatedAt",model.updatedAt.ToDBValue()),new SqlParameter("@createdAt",model.createdAt.ToDBValue()))>0;
}
 #endregion
#region ��ѯ���� +int QueryCount(object wheres)
/// <summary>
/// ��ѯ����
/// </summary>
/// <param name="wheres">��ѯ����</param>
/// <returns>����</returns>
public int QueryCount(object wheres)
{
List<SqlParameter> list=null;
string str = wheres.parseWheres(out list);
str=str==""? str:"where "+str;
string sql = "SELECT COUNT(1) from item "+str;var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
return res == null ? 0 : Convert.ToInt32(res);
}
#endregion
#region ��ѯ����ģ��ʵ�� +item QuerySingleById(string objectId)
/// <summary>
/// ��ѯ����ģ��ʵ��
/// </summary>
/// <param name="id">objectId</param>);
/// <returns>ʵ��</returns>);
public item QuerySingleById(string objectId)
{
const string sql = "SELECT TOP 1 objectId,_UserId,title,price,discount,details,type,mall_name,label,hot,love,reward,pic_a,pic_b,pic_c,url,commentCount,state,admin,updatedAt,createdAt from item WHERE [objectId] = @objectId";
using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@objectId", objectId)))
{
if (reader.HasRows){
reader.Read();
item model = SqlHelper.MapEntity<item>(reader);
_UserDAO _UserDAO = new _UserDAO();
model._User = _UserDAO.QuerySingleById((string)reader["_UserId"]);
return model;
}
else
{
return null;
}
}
}
#endregion
#region ��ѯ����ģ��ʵ�� +_User QuerySingleByIdX(string objectId,string columns){
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name=objectId>����</param>);
///<returns>ʵ��</returns>);
public Dictionary<string,object> QuerySingleByIdX(string objectId, object columns)
{
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","title","price","discount","details","type","mall_name","label","hot","love","reward","pic_a","pic_b","pic_c","url","commentCount","state","admin","updatedAt","createdAt" };
string[] cls = columns.parseColumnsX(clumns,"item", out li);
string sql = "SELECT TOP 1 "+string.Join(",", li["item"])+" from item WHERE [objectId] = @objectId";
using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@objectId", objectId)))
{
if (reader.HasRows)
{
reader.Read();
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);
if(li.ContainsKey("_User")){
_UserDAO _UserDAO = new _UserDAO();
model["_User"] = _UserDAO.QuerySingleByIdX((string)reader["_UserId"],li["_User"]);
}

return model;
}
else
{
return null;
}
}
}
#endregion
#region ��ѯ����ģ��ʵ�� +Users QuerySingleByWheres(object wheres)
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name="wheres">����������</param>
///<returns>ʵ��</returns>
public item QuerySingleByWheres(object wheres)
{
var list = QueryList(1, 1, wheres);
return list != null && list.Any() ? list.FirstOrDefault() : null;
}
#endregion
#region ��ѯ����ģ���м��� +Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name="wheres">����</param>
///<param name="columns">�м���</param>
///<returns>ʵ��</returns>
public Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
where = string.IsNullOrEmpty(where) ? "" : " where " + where;
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","title","price","discount","details","type","mall_name","label","hot","love","reward","pic_a","pic_b","pic_c","url","commentCount","state","admin","updatedAt","createdAt" };
string[] cls = columns.parseColumnsX(clumns,"item", out li);
string sql = "SELECT TOP 1 "+string.Join(",", li["item"])+" from item"+where;
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
reader.Read();
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);
if(li.ContainsKey("_User")){
_UserDAO _UserDAO = new _UserDAO();
model["_User"] = _UserDAO.QuerySingleByIdX((string)reader["_UserId"],li["_User"]);
}

return model;
}
else
{
return null;
}
}
}
#endregion
#region ��ҳ��ѯһ������ +IEnumerable<Users> QueryList(int index, int size, object wheres=null, string orderField=id, bool isDesc = true)
///<summary>
///��ҳ��ѯһ������
///</summary>
///<param name="index">ҳ��</param>
///<param name="size">ҳ��С</param>
///<param name="wheres">����������</param>
///<param name="orderField">�����ֶ�</param>
///<param name="isDesc">�Ƿ�������</param>
///<returns>ʵ�弯��</returns>
public IEnumerable<item> QueryList(int index, int size, object wheres=null, string orderField="objectId", bool isDesc = true)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
orderField=string.IsNullOrEmpty(orderField) ? "objectId" : orderField;
var sql = SqlHelper.GenerateQuerySql("item",new string[]{"objectId","_UserId","title","price","discount","details","type","mall_name","label","hot","love","reward","pic_a","pic_b","pic_c","url","commentCount","state","admin","updatedAt","createdAt"}, index, size, where, orderField, isDesc);
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
while (reader.Read())
{
item model = SqlHelper.MapEntity<item>(reader);
_UserDAO _UserDAO = new _UserDAO();
model._User = _UserDAO.QuerySingleById((string)reader["_UserId"]);
yield return model;
}
}
}
}
#endregion
#region ��ҳ��ѯһ������ +IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField=id, bool isDesc = true)
///<summary>
///��ҳ��ѯһ������
///</summary>
///<param name="index">ҳ��</param>
///<param name="size">ҳ��С</param>
///<param name="columns">ָ������</param>
///<param name="wheres">����������</param>
///<param name="orderField">�����ֶ�</param>
///<param name="isDesc">�Ƿ�������</param>
///<returns>ʵ�弯��</returns>
public IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField="objectId", bool isDesc = true)
{
List<SqlParameter> list = null;
string where = wheres.parseWheres(out list);
orderField=string.IsNullOrEmpty(orderField) ? "objectId" : orderField;
Dictionary<string, string[]> li;
string[] clumns = new String[] { "objectId","title","price","discount","details","type","mall_name","label","hot","love","reward","pic_a","pic_b","pic_c","url","commentCount","state","admin","updatedAt","createdAt" };
string[] cls = columns.parseColumnsX(clumns,"item", out li);
var sql = SqlHelper.GenerateQuerySql("item", li["item"], index, size, where, orderField, isDesc);
using (var reader = SqlHelper.ExecuteReader(sql,list.ToArray()))
{
if (reader.HasRows)
{
while (reader.Read())
{
Dictionary<string, object> model = SqlHelper.MapEntity(reader, cls);
if(li.ContainsKey("_User")){
_UserDAO _UserDAO = new _UserDAO();
model["_User"] = _UserDAO.QuerySingleByIdX((string)reader["_UserId"],li["_User"]);
}

yield return model;
}
}
}
}
#endregion
#region ���������޸�ָ���� +bool UpdateById(string objectId,object columns)
/// <summary>
/// ������������ָ����¼
/// </summary>
/// <param name="objectId">����</param>
/// <param name="columns">�м��϶���</param>
/// <returns>�Ƿ�ɹ�</returns>
public bool UpdateById(string objectId, object columns)
{
List<SqlParameter> list = null;
string[] column = columns.parseColumns(out list);
list.Add(new SqlParameter("@objectId", objectId.ToDBValue()));
string sql = string.Format(@"UPDATE [dbo].[item] SET  {0}  WHERE [{1}] = @{1}", string.Join(",", column), "objectId");
return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
}
 #endregion
#region �����������¼�¼+bool UpdateByWheres(object wheres, object columns)
/// <summary>
/// �����������¼�¼
/// </summary>
/// <param name="wheres">��������ʵ��ʵ��</param>
/// <param name="columns">�м��϶���</param>
/// <returns>�Ƿ�ɹ�</returns>
public bool UpdateByWheres(object wheres, object columns)
{
List<SqlParameter> list = null;
string[] column = columns.parseColumns(out list);
List<SqlParameter> list1 = null;
string where = wheres.parseWheres(out list1);
where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
list.AddRange(list1);
string sql = string.Format(@"UPDATE [dbo].[item] SET  {0} {1}", string.Join(",", column), where);
return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
}
 #endregion
}
}