using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.PublicTools
{
    public class DBHelper
    {
        private string strSQL;
        private SqlConnection Conn;            //数据库Connection对象
        private SqlCommand SqlCmd;                //数据库OdbcCommand对象
        private DataSet ds = new DataSet();       //返回数据集
        private SqlDataAdapter SqlDataAdapter;    //数据库DataAdapter对象
        private int outTime = 10 * 60;//秒

        private string conn_str = "";
        string str = ConfigurationManager.AppSettings["PMS_WeddingEntities"];
        public DBHelper()
        {
            if (str != null)
            {

                conn_str = str;

            }
        }



        public DBHelper getInstance
        {
            get
            {
                DBHelper db = new DBHelper();
                return db;
            }
        }

        #region 打开数据库连接
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void OpenConn()
        {
            try
            {
                this.Conn = new SqlConnection(conn_str);
                if ((this.Conn != null) && (this.Conn.State != ConnectionState.Open))
                {
                    this.Conn.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 关闭数据库连接
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void CloseConn()
        {
            try
            {
                if ((this.Conn != null) && (this.Conn.State == ConnectionState.Open))
                {
                    Conn.Dispose();
                    this.Conn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        #region  返回DataTable Pars
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="tempStrSQL">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable ExcuteForDataTable(string tempStrSQL, IDataParameter[] pars)
        {
            this.OpenConn();
            DataTable dt = new DataTable();
            this.strSQL = tempStrSQL;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = tempStrSQL;
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                this.SqlDataAdapter = new SqlDataAdapter(cmd);
                this.SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                this.SqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
            return dt;
        }
        #endregion

        #region  返回DataTable
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="tempStrSQL">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable ExcuteForDataTable(string tempStrSQL)
        {
            this.OpenConn();
            DataTable dt = new DataTable();
            this.strSQL = tempStrSQL;
            try
            {
                this.SqlDataAdapter = new SqlDataAdapter(this.strSQL, this.Conn);
                this.SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                this.SqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
            return dt;
        }
        #endregion

        #region 返回List<T,Pars>

        /// <summary>
        /// 返回List<T>
        /// </summary>
        /// <param name="tempStrSQL">查询语句</param>
        /// <returns>List</returns>
        public List<T> ExcuteForList<T>(string tempStrSQL, SqlParameter[] pars) where T : new()
        {
            this.OpenConn();
            DataTable dt = new DataTable();
            this.strSQL = tempStrSQL;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                this.SqlDataAdapter = new SqlDataAdapter(cmd);
                this.SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                this.SqlDataAdapter.Fill(dt);
                return ConvertHelper.ToList<T>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 返回List<T>

        /// <summary>
        /// 返回List<T>
        /// </summary>
        /// <param name="tempStrSQL">查询语句</param>
        /// <returns>List</returns>
        public List<T> ExcuteForList<T>(string tempStrSQL) where T : new()
        {
            this.OpenConn();
            DataTable dt = new DataTable();
            this.strSQL = tempStrSQL;
            try
            {
                this.SqlDataAdapter = new SqlDataAdapter(this.strSQL, this.Conn);
                this.SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                this.SqlDataAdapter.Fill(dt);
                return ConvertHelper.ToList<T>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 返回 List<Dictionary<string, string>>
        /// <summary>
        /// 返回 List<Dictionary<string, string>>
        /// </summary>
        /// <param name="tempStrSQL">查询语句</param>
        /// <returns>List</returns>
        public List<Dictionary<string, string>> ExcuteForListDic(string tempStrSQL)
        {
            this.OpenConn();
            DataTable dt = new DataTable();
            this.strSQL = tempStrSQL;
            try
            {
                this.SqlDataAdapter = new SqlDataAdapter(this.strSQL, this.Conn);
                this.SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                this.SqlDataAdapter.Fill(dt);
                return ConvertHelper.ToListDictionary(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 返回首行首列[object]
        /// <summary>
        /// 返回首行首列[object]
        /// </summary>
        /// <param name="tempStrSQL">SQL语句</param>
        /// <returns>返回首行首列</returns>
        public object ExcuteScalar(string tempStrSQL)
        {
            this.OpenConn();
            this.strSQL = tempStrSQL;
            try
            {
                SqlCmd = new SqlCommand(tempStrSQL, this.Conn);
                SqlCmd.CommandTimeout = outTime;
                return SqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 返回更新条数[int]
        /// <summary>
        /// 返回更新条数[int]
        /// </summary>
        /// <param name="tempStrSQL">SQL语句</param>
        /// <returns>更新条数</returns>
        public int ExcuteNonQuery(string tempStrSQL)
        {
            this.OpenConn();
            this.strSQL = tempStrSQL;
            int intNumber = 0;
            try
            {
                SqlCmd = new SqlCommand(tempStrSQL, this.Conn);
                SqlCmd.CommandTimeout = outTime;
                intNumber = SqlCmd.ExecuteNonQuery();
                return intNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }

        }
        #endregion

        #region 返回更新条数[int,Pars]
        /// <summary>
        /// 返回更新条数[int]
        /// </summary>
        /// <param name="tempStrSQL">SQL语句</param>
        /// <returns>更新条数</returns>
        public int ExcuteNonQuery(string tempStrSQL, SqlParameter[] pars)
        {
            this.OpenConn();
            this.strSQL = tempStrSQL;
            int intNumber = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandTimeout = outTime;
                intNumber = cmd.ExecuteNonQuery();
                return intNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }

        }
        #endregion

        #region ExcuteInsertReturnAutoID
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="tempStrSQL"></param>
        /// <returns></returns>
        public int ExcuteInsertReturnAutoID(string tempStrSQL)
        {
            this.OpenConn();
            this.strSQL = tempStrSQL + ";select last_insert_rowid();";
            int intNumber = 0;
            try
            {
                SqlCmd = new SqlCommand(tempStrSQL, this.Conn);
                SqlCmd.CommandTimeout = outTime;
                intNumber = Convert.ToInt32(SqlCmd.ExecuteScalar());
                return intNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 返回是否存在记录[bool]
        /// <summary>
        /// 返回是否存在记录[bool]
        /// </summary>
        /// <param name="tempStrSQL">SQL语句</param>
        /// <returns>是否存在</returns>
        public bool ExcuteExist(string tempStrSQL)
        {
            this.OpenConn();
            this.strSQL = tempStrSQL;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter = new SqlDataAdapter(tempStrSQL, this.Conn);
                SqlDataAdapter.SelectCommand.CommandTimeout = outTime;
                SqlDataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 插入多条记录
        /// <summary>
        /// 插入多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int ExcuteInsert<T>(List<T> data, string tableName)
        {
            try
            {
                if (data.Count == 0)
                    return 0;
                this.OpenConn();
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = Conn;
                SqlCmd.CommandTimeout = outTime;
                SqlCmd.CommandType = CommandType.Text;
                PropertyInfo[] propertys = data[0].GetType().GetProperties();
                if (propertys.Count() == 0)
                    return 0;
                strSQL = "insert into " + tableName + "(";
                foreach (PropertyInfo pi in propertys)
                {
                    strSQL += pi.Name;
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                strSQL += ("values(");
                foreach (PropertyInfo pi in propertys)
                {
                    strSQL += ("@" + pi.Name);
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                SqlCmd.CommandText = strSQL;
                foreach (T t in data)
                {
                    propertys = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertys)
                    {
                        SqlParameter para = new SqlParameter();
                        para.ParameterName = "@" + pi.Name;
                        if (pi.GetType() == typeof(DateTime))
                            para.Value = ((DateTime)pi.GetValue(t, null)).ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            para.Value = pi.GetValue(t, null);
                        SqlCmd.Parameters.Add(para);
                    }
                }
                int result = SqlCmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 插入多条记录-可以设置不插入的特殊字段
        /// <summary>
        /// 插入多条记录-可以设置不插入的特殊字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <param name="exExceptFiled"></param>
        /// <returns></returns>
        public int ExcuteInsert<T>(List<T> data, string tableName, string exExceptFiled)
        {
            try
            {
                if (data.Count == 0)
                    return 0;
                string[] exExFiledArr = exExceptFiled.Split(',');
                this.OpenConn();
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = Conn;
                SqlCmd.CommandTimeout = outTime;
                SqlCmd.CommandType = CommandType.Text;
                PropertyInfo[] propertys = data[0].GetType().GetProperties();
                if (propertys.Count() == 0)
                    return 0;
                strSQL = "insert into " + tableName + "(";
                foreach (PropertyInfo pi in propertys)
                {
                    if (exExFiledArr.Contains(pi.Name))
                        continue;
                    strSQL += pi.Name;
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                if (strSQL.EndsWith(","))
                    strSQL = strSQL.Substring(0, strSQL.Length - 1) + ")";
                strSQL += ("values(");
                foreach (PropertyInfo pi in propertys)
                {
                    if (exExFiledArr.Contains(pi.Name))
                        continue;
                    strSQL += ("@" + pi.Name);
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                if (strSQL.EndsWith(","))
                    strSQL = strSQL.Substring(0, strSQL.Length - 1) + ")";
                SqlCmd.CommandText = strSQL;
                foreach (T t in data)
                {
                    propertys = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (exExFiledArr.Contains(pi.Name))
                            continue;
                        SqlParameter para = new SqlParameter();
                        para.ParameterName = "@" + pi.Name;
                        if (pi.PropertyType == typeof(DateTime))
                            para.Value = ((DateTime)pi.GetValue(t, null)).ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            para.Value = pi.GetValue(t, null);
                        SqlCmd.Parameters.Add(para);
                    }
                }
                int result = SqlCmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 插入单条记录-可以设置不插入的特殊字段
        /// <summary>
        /// 插入单条记录-可以设置不插入的特殊字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <param name="exExceptFiled"></param>
        /// <returns></returns>
        public int ExcuteInsert<T>(T data, string tableName, string exExceptFiled)
        {
            try
            {
                return ExcuteInsert<T>(new List<T>() { data }, tableName, exExceptFiled);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 插入单条记录
        /// <summary>
        ///  插入单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int ExcuteInsert<T>(T data, string tableName)
        {
            try
            {
                return ExcuteInsert<T>(new List<T>() { data }, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion

        #region 插入单条记录并返回自增列ID
        /// <summary>
        /// 插入单条记录并返回自增列ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <param name="exExceptFiled"></param>
        /// <returns></returns>
        public int ExcuteInsertReturnAutoID<T>(T data, string tableName, string exExceptFiled)
        {
            try
            {
                string[] exExFiledArr = exExceptFiled.Split(',');
                this.OpenConn();
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = Conn;
                SqlCmd.CommandTimeout = outTime;
                SqlCmd.CommandType = CommandType.Text;
                PropertyInfo[] propertys = data.GetType().GetProperties();
                if (propertys.Count() == 0)
                    return 0;
                strSQL = "insert into " + tableName + "(";
                foreach (PropertyInfo pi in propertys)
                {
                    if (exExFiledArr.Contains(pi.Name))
                        continue;
                    strSQL += pi.Name;
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                if (strSQL.EndsWith(","))
                    strSQL = strSQL.Substring(0, strSQL.Length - 1) + ")";
                strSQL += ("values(");
                foreach (PropertyInfo pi in propertys)
                {
                    if (exExFiledArr.Contains(pi.Name))
                        continue;
                    strSQL += ("@" + pi.Name);
                    if (propertys.LastOrDefault() != pi)
                        strSQL += ",";
                    else
                        strSQL += ")";
                }
                if (strSQL.EndsWith(","))
                    strSQL = strSQL.Substring(0, strSQL.Length - 1) + ")";
                strSQL += ";select @@Identity;";
                SqlCmd.CommandText = strSQL;

                propertys = data.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (exExFiledArr.Contains(pi.Name))
                        continue;
                    SqlParameter para = new SqlParameter();
                    para.ParameterName = "@" + pi.Name;
                    if (pi.GetType() == typeof(DateTime))
                        para.Value = ((DateTime)pi.GetValue(data, null)).ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        para.Value = pi.GetValue(data, null);
                    SqlCmd.Parameters.Add(para);
                }

                int result = Convert.ToInt32(SqlCmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConn();
            }
        }
        #endregion
    }
}
