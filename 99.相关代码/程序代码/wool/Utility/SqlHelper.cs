using wool.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace wool.Utility
{
    /// <summary>
    /// SQL Server数据库访问助手类
    /// 本类为静态类 不可以被实例化 需要使用时直接调用即可
    /// </summary>
    public static partial class SqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly static string connStr;
        static SqlHelper()
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        }


        #region 生成查询语句
  #region 生成分页查询数据库语句 +static string GenerateQuerySql(string table, string[] columns, int index, int size, string wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成分页查询数据库语句, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合, 多个列用英文逗号分割(colum1,colum2...)</param>
        /// <param name="index">页码(即第几页)(传入-1则表示忽略分页取出全部)</param>
        /// <param name="size">显示页面大小(即显示条数)</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, int index, int size, string where, string orderField, bool isDesc = true)
        {
            if (index == 1)
            {
                // 生成查询第一页SQL
                return GenerateQuerySql(table, columns, size, where, orderField, isDesc);
            }
            if (index < 1)
            {
                // 取全部数据
                return GenerateQuerySql(table, columns, where, orderField, isDesc);
            }
            // 其他情况, 生成row_number分页查询语句
            // SQL模版
            const string format = @"select {0} from
                                    (
                                        select ROW_NUMBER() over(order by [{1}] {2}) as num, {0} from [{3}] {4}
                                    )
                                    as tbl
                                    where
                                        tbl.num between ({5}-1)*{6} + 1 and {5}*{6};";
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            return string.Format(format, column, orderField, isDesc ? "desc" : string.Empty, table, where, index, size);
        }
        #endregion    
        #region 生成分页查询数据库语句 +static string GenerateQuerySqlX(string table, string[] columns,string[] columns1, int index, int size, string[] wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成分页查询数据库语句, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合, 多个列用英文逗号分割(colum1,colum2...)</param>
        /// <param name="index">页码(即第几页)(传入-1则表示忽略分页取出全部)</param>
        /// <param name="size">显示页面大小(即显示条数)</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySqlX(string[] tables,string[] columns, int index, int size, string[] wheres, string orderField, bool isDesc = false)
        {
           
            // 其他情况, 生成row_number分页查询语句
            // SQL模版 0.tbl.id,UserRoles.id
            const string format = @"select {0} from
                                    (
                                        select ROW_NUMBER() over(order by {1}) as num, * from {2} {3}
                                    )
                                    as tbl,{4}
                                    where
                                        tbl.num between ({5}-1)*{6} + 1 and {5}*{6} and {7};";
            
            // 查询字段拼接
            string column = columns != null ? string.Join(" , ", columns) : "";
            //排序字段
            string orderBy=orderField+(isDesc ? " desc" : string.Empty);
            //条件
            wheres[0] = string.IsNullOrEmpty(wheres[0]) ? "":"where " + wheres[0]; 

            return string.Format(format, column, orderBy, tables[0], wheres[0],tables[1], index, size, wheres[1]);
        }
        #endregion
        #region 生成查询数据库语句查询全部 +static string GenerateQuerySql(string table, string[] columns, string wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成查询数据库语句查询全部, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, string where, string orderField, bool isDesc = true)
        {
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            const string format = "select {0} from {1} {2} {3} {4}";
            return string.Format(format, column, table, where,
                string.IsNullOrEmpty(orderField) ? string.Empty : "order by " + orderField,
                isDesc && !string.IsNullOrEmpty(orderField) ? "desc" : string.Empty);
        }
        #endregion
        #region 生成分页查询数据库语句查询第一页 +static string GenerateQuerySql(string table, string[] columns, int size, string where, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成分页查询数据库语句查询第一页, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合</param>
        /// <param name="size">显示页面大小(即显示条数)</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, int size, string where, string orderField, bool isDesc = true)
        {
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            const string format = "select top {0} {1} from {2} {3} {4} {5}";
            return string.Format(format, size, column, table, where,
                  string.IsNullOrEmpty(orderField) ? string.Empty : "order by " + orderField,
                  isDesc && !string.IsNullOrEmpty(orderField) ? "desc" : string.Empty);
        }
        #endregion
        #endregion

        #region sqlDataReader转化成对象  MapEntity
        #region  +static Dictionary<string,object> MapEntityX<TEntity>(SqlDataReader reader) where TEntity : class,new()
        /// <summary>
        /// 将一个SqlDataReader对象转换成一个实体类对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="reader">当前指向的reader</param>
        /// <returns>实体对象</returns>
        public static Dictionary<string,object> MapEntityX<TEntity>(SqlDataReader reader) where TEntity : class,new()
        {
            try
            {
                var props = typeof(TEntity).GetProperties();
                Dictionary<string, object> entity = new Dictionary<string,object>();
                foreach (var prop in props)
                {

                    if (prop.CanWrite && (prop.PropertyType.IsValueType || typeof(string) == prop.PropertyType))
                    {
                        try
                        {
                            var index = reader.GetOrdinal(prop.Name);
                            var data = reader.GetValue(index);
                            if (data != DBNull.Value)
                            {
            //                    if (prop.PropertyType.IsGenericType &&
            //prop.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            //                    {

            //                        //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
            //                        NullableConverter nullableConverter = new NullableConverter(prop.PropertyType);
            //                        //将convertsionType转换为nullable对的基础基元类型

            //                        prop.SetValue(entity, Convert.ChangeType(data, nullableConverter.UnderlyingType), null);
            //                    }
            //                    else
            //                    {
            //                        prop.SetValue(entity, Convert.ChangeType(data, prop.PropertyType), null);
            //                    }
                                //if (prop.PropertyType.Equals(typeof(DateTime)))
                                //{
                                //    entity[prop.Name] = ((DateTime)data).ToString("yyyy-mm-dd HH:mm:ss");
                                //}
                                //else {
                                //    entity[prop.Name] = data;
                                //}
                                entity[prop.Name] = data;
                                
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
                return entity;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region  +static TEntity MapEntity<TEntity>(SqlDataReader reader) where TEntity : class,new()
        /// <summary>
        /// 将一个SqlDataReader对象转换成一个实体类对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="reader">当前指向的reader</param>
        /// <returns>实体对象</returns>
        public static TEntity MapEntity<TEntity>(SqlDataReader reader) where TEntity : class,new()
        {
            try
            {
                var props = typeof(TEntity).GetProperties();
                var entity = new TEntity();
                foreach (var prop in props)
                {

                    if (prop.CanWrite && (prop.PropertyType.IsValueType || typeof(string) == prop.PropertyType))
                    {
                        try
                        {
                            var index = reader.GetOrdinal(prop.Name);
                            var data = reader.GetValue(index);
                            if (data != DBNull.Value)
                            {
                                if (prop.PropertyType.IsGenericType &&
            prop.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                {

                                    //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                    NullableConverter nullableConverter = new NullableConverter(prop.PropertyType);
                                    //将convertsionType转换为nullable对的基础基元类型

                                    prop.SetValue(entity, Convert.ChangeType(data, nullableConverter.UnderlyingType), null);
                                }
                                else {
                                    prop.SetValue(entity, Convert.ChangeType(data, prop.PropertyType), null);
                                }
                                
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
                return entity;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region  +static Dictionary<string, object> MapEntity<TEntity>(SqlDataReader reader,string[] lists) where TEntity : class,new()
        /// <summary>
        /// 将一个SqlDataReader对象转换成一个实体类对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="reader">当前指向的reader</param>
        /// <returns>实体对象</returns>
        public static Dictionary<string, object> MapEntity(SqlDataReader reader,string[] lists) 
        {
            try
            {
                Dictionary<string, object> entity = new Dictionary<string, object>();
                foreach (string list in lists)
                {

                    try
                        {
                            var index = reader.GetOrdinal(list);
                            var data = reader.GetValue(index).FromDBValue();
                            entity.Add(list, data);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                }
                return entity;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #endregion

        #region SQL执行方法

        #region ExecuteNonQuery +static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteNonQuery +static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
				conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandType = type;
                    try
                    {
                        int res = cmd.ExecuteNonQuery();
                        return res;
                        
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region ExecuteScalar +static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteScalar +static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
				conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandType = type;
                    try
                    {
                        object res = cmd.ExecuteScalar();
                        return res;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion


        #region ExecuteReader +static void ExecuteReader(string cmdText, Action<SqlDataReader> action, params SqlParameter[] parameters)
        /// <summary>
        /// 利用委托 执行一个大数据查询的T-SQL语句
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        public static void ExecuteReader(string cmdText, Action<SqlDataReader> action, params SqlParameter[] parameters)
        {
            ExecuteReader(cmdText, action, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteReader +static void ExecuteReader(string cmdText, Action<SqlDataReader> action, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 利用委托 执行一个大数据查询的T-SQL语句
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        public static void ExecuteReader(string cmdText, Action<SqlDataReader> action, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
					conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandType = type;
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            action(reader);
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region ExecuteReader +static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个SqlDataReader对象, 如果出现SQL语句执行错误, 将会关闭连接通道抛出异常
        ///  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteReader +static SqlDataReader ExecuteReader(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个SqlDataReader对象, 如果出现SQL语句执行错误, 将会关闭连接通道抛出异常
        ///  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText, CommandType type, params SqlParameter[] parameters)
        {         
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandType = type;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
            }  
        }
        #endregion


        #region ExecuteDataSet +static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataSet +static DataSet ExecuteDataSet(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = type;
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        #endregion

        #region ExecuteDataTable +static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataTable +static DataTable ExecuteDataTable(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, type, parameters).Tables[0];
        }
        #endregion
        #endregion

        #region 事务处理
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuerysTransaction(string cmdText, SqlParameter[] parameters,string cmdText1, params SqlParameter[] parameters1)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //con必须打开，负责会报错
                    SqlTransaction trans = conn.BeginTransaction();
                    cmd.Transaction = trans;

                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        //事务一
                        cmd.CommandText = cmdText;
                        cmd.Parameters.AddRange(parameters);
                        int res = cmd.ExecuteNonQuery();
                        //事务一

                        cmd.CommandText = cmdText1;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters1);
                        int res1 = cmd.ExecuteNonQuery();
                        //提交事务
                        trans.Commit();

                        return res * 10 + res1;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        //回滚事务
                        trans.Rollback();
                        throw e;
                    }
                }
            }
        }
      
        #endregion

        #region  parseWheres parseColumns
        public static string parseWheres(this object wheres, out List<SqlParameter> list)
        {          
            string returnValue = "";
            list = new List<SqlParameter>();

            if (wheres==null) return returnValue;
            //单条件
            if (wheres is Wheres)
            {
                Wheres wh = (Wheres)wheres;
                list.Add(new SqlParameter("@" + wh.key, wh.value));
                return string.Format(" {0} {1} {2} @{1} ", wh.relation, wh.key, wh.operation);
            }
            //多条件
            List<Wheres> li = (List<Wheres>)wheres;
            foreach (Wheres whs in li)
            {
                returnValue += string.Format(" {0} {1} {2} @{1} ",whs.relation, whs.key, whs.operation);
                list.Add(new SqlParameter("@" + whs.key, whs.value));
            }
            
            return returnValue;
        }
        public static string[] parseColumnsX(this object columns,string[] clumns, string tableName, out Dictionary<string, string[]> li)
        {
            li = new Dictionary<string, string[]>();
            if (columns == null)
            {
                li[tableName] = clumns;
            }
            else {
                if (columns is string[])
                {
                    li[tableName] = (string[])columns;
                }
                else {

                    li = (Dictionary<string, string[]>)columns;
                }
            }
            
            if (!li.Keys.Contains(tableName))
            {
                li[tableName] = clumns;
            }
            //获取数据
            string[] retu = li[tableName];
            //查询数据
            List<string> cls1 = new List<string>();
            cls1.AddRange(li[tableName]);
            foreach (var key in li.Keys)
            {
                if (key != tableName)
                {
                    cls1.Add(key + "Id");
                }

            }
            li[tableName] = cls1.ToArray();

            return retu;
        }
        public static string[] parseColumns(this object columns, out List<SqlParameter> list)
        {
           
            list = new List<SqlParameter>();

            if (columns == null) return null;
            Dictionary<string, object> li = (Dictionary<string, object>)columns;
            int num = li.Count;
            string[] str = new string[num];
            int i = 0;
            foreach (var ele in li) {
                str[i] = string.Format(" {0} = @{0} ", ele.Key);
                list.Add(new SqlParameter("@" + ele.Key, ele.Value));
                i++;
            }
            return str;
        }
        #endregion

        #region ToDBValue FromDBValue
        public static object ToDBValue(this object value)
        {
            return value == null ? DBNull.Value : value;
        }

        public static object FromDBValue(this object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }
        #endregion
    }
}
