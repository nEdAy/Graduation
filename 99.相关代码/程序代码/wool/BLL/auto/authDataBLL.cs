using System.Linq;
using System.Collections.Generic;
using wool.DAO;
using wool.Model;
namespace wool.BLL{
public partial class authDataBLL{
/// <summary>
/// ���ݿ��������
/// </summary>
private authDataDAO _dao = new authDataDAO();
#region �����ݿ������һ����¼ +bool; Insert(authData model)
/// <summary>
/// �����ݿ������һ����¼
/// </summary>
/// <param name="model">Ҫ��ӵ�ʵ��</param>
/// <returns>�Ƿ�ɹ�</returns>
public bool Insert(authData model)
{
return _dao.Insert(model);
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
return _dao.Delete(objectId);
}
#endregion
#region ��������ID����һ����¼ +bool Update(authData model)
/// <summary>
/// ������������һ����¼
/// </summary>
/// <param name="model">���º��ʵ��</param>
/// <returns>ִ�н����Ӱ������</returns>
public bool Update(authData model)
{
return _dao.Update(model);
}
#endregion
#region ��ѯ���� +int QueryCount(object wheres)
/// <summary>
/// ��ѯ����
/// </summary>
/// <param name="wheres">��ѯ����</param>
/// <returns>ʵ��</returns>
public int QueryCount(object wheres)
{
return _dao.QueryCount(wheres);
}
#endregion

#region ��ҳ��ѯһ������ +IEnumerable<Users> QueryList(int index, int size, object wheres=null, string orderField=null, bool isDesc = false)
///<summary>
///��ҳ��ѯһ������
///</summary>
///<param name="index">ҳ��</param>
///<param name="size">ҳ��С</param>
///<param name="wheres">����������</param>
///<param name="orderField">�����ֶ�</param>
///<param name="isDesc">�Ƿ�������</param>
///<returns>ʵ�弯��</returns>
public IEnumerable<authData> QueryList(int index, int size=10, object wheres=null, string orderField=null, bool isDesc=false)
{
return _dao.QueryList(index, size, wheres, orderField, isDesc);
}
#endregion
#region ��ҳ��ѯһ������ +IEnumerable<Users> QueryListX(int index, int size,object columns=null, object wheres=null, string orderField=null, bool isDesc = false)
///<summary>
///��ҳ��ѯһ������
///</summary>
///<param name="index">ҳ��</param>
///<param name="size">ҳ��С</param>
///<param name="wheres">����������</param>
///<param name="orderField">�����ֶ�</param>
///<param name="isDesc">�Ƿ�������</param>
///<returns>ʵ�弯��</returns>
public IEnumerable<Dictionary<string, object>> QueryListX(int index, int size=10,object columns=null, object wheres=null, string orderField=null, bool isDesc=false)
{
return _dao.QueryListX(index, size,columns, wheres, orderField, isDesc);
}
#endregion
#region ��ѯ����ģ��ʵ�� +authData QuerySingleByWheres(object wheres)
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name="wheres">����������</param>
///<returns>ʵ��</returns>
public authData QuerySingleByWheres(object wheres)
{
return _dao.QuerySingleByWheres(wheres);
}
#endregion
#region ��ѯ����ģ��ʵ�� +Dictionary<string,object> QuerySingleByWheresX(object wheres,object columns)
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name="wheres">����</param>
///<param name="columns">�м���</param>
///<returns>ʵ��</returns>
public Dictionary<string,object> QuerySingleByWheresX(object wheres,object columns)
{
return _dao.QuerySingleByWheresX(wheres,columns);
}
#endregion
#region ��ѯ����ģ��ʵ�� +authData QuerySingleById(string objectId)
///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name="objectId">key</param>
///<returns>ʵ��</returns>
public authData QuerySingleById(string objectId){
return _dao.QuerySingleById(objectId);
}
#endregion
#region ��ѯ����ģ��ʵ�� +Dictionary<string,object> QuerySingleByIdX(string objectId,object columns)///<summary>
///��ѯ����ģ��ʵ��
///</summary>
///<param name=objectId>key</param>
///<returns>ʵ��</returns>
///<returns>ʵ��</returns>
public Dictionary<string,object> QuerySingleByIdX(string objectId, object columns)
{
return _dao.QuerySingleByIdX(objectId,columns);
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
return _dao.UpdateById(objectId, columns);
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
return _dao.UpdateByWheres(wheres, columns);
}
 #endregion
}
}