//=================================
//【微软.NET数据访问程序】
// SqlHelper：执行各种方式的数据操作处理
// SqlHelperParameterCache：获得存储过程的参数集合
//=================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;

namespace ObAuto
{
    /// <summary>
    /// SqlHelper 个人修改版
    /// </summary>
    public sealed class MsSqlHelper
    {
        public static string connString;
        public static SqlConnection conn;

        private MsSqlHelper() { }

        #region _初始化数据库连接_连接操作
        public static void ConnectionString(string _connString)
        {
            connString = _connString;
        }
        public static SqlConnection Connection
        {
            get
            {
                if (conn == null)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();
                }
                else if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();
                }
                else if (conn.State == System.Data.ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn;
            }
            //set 
            //{
            //    conn = new SqlConnection(connString);
            //}
        }
        public static void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        #endregion

        #region 私有公共方法
        /// <summary>
        /// 将命令对象和一组参数对象联系起来
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        private static void AttachParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            foreach (SqlParameter param in parameters)
            {
                if ((param.Direction == ParameterDirection.InputOutput) && (param.Value == null))
                {
                    param.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param);
            }
        }

        /// <summary>
        /// 给一组参数对象赋值
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="values"></param>
        private static void AssignParameterValues(SqlParameter[] parameters, object[] values)
        {
            if ((parameters == null) || (values == null))
            {
                return;
            }
            if (parameters.Length != values.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count!");
            }
            for (int i = 0, j = parameters.Length; i < j; i++)
            {
                parameters[i].Value = values[i];
            }
        }

        private static void PrepareCommand(SqlCommand command, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            command.Connection = Connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (parameters != null)
            {
                AttachParameters(command, parameters);
            }
        }
        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="prefix">前缀，如##或#</param>
        /// <param name="createTable"></param>
        /// <returns></returns>
        public static bool IsTableExists(string tableName, string prefix, bool createTable)
        {
            //tableName = @"##" + tableName;
            prefix = string.IsNullOrEmpty(prefix) ? "" : prefix;
            tableName = prefix + tableName;
            SqlConnection sqlconn = new SqlConnection(connString);
            string sql = "select * from tempdb.dbo.sysobjects where type='U' and name = '" + tableName + "'";
            SqlDataAdapter sqlda = new SqlDataAdapter(sql, sqlconn);
            DataSet myds = new DataSet();
            sqlda.Fill(myds);
            if (myds.Tables[0].Rows.Count == 0)
            {
                if (createTable)
                {
                    string sqlCreate = "CREATE TABLE " + tableName + @"(ID VARCHAR(11))";
                    ExecuteCommand(sqlCreate);
                }
                return false;
            }
            else return true;
        }

        #endregion

        #region datatable列名转换
        public static DataTable ChgColNameToCN(DataTable dt)
        {
            DataTable _dt = dt;
            int colcount = _dt.Columns.Count;
            try
            {
                for (int i = 0; i < colcount; i++)
                {
                    _dt.Columns[i].ColumnName = _dt.Columns[i].Caption;
                }
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }
        #endregion

        #region 数据操作：ExecuteNonQuery （存储过程）
        /// <summary>
        /// 1.无参数数据操作，或直接的SQL语句
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sqlString)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// 2.带参数数据操作，默认参数属性
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sqlString, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// 3.无参数带操作数据操作（存储过程）
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <returns></returns>
        public static int ExecuteCommand(string sqlTypeString, CommandType cmdType)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// 4.带参数带操作数据操作（存储过程）
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sqlTypeString, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                return -1;
                throw ex;
            }
        }
        #endregion

        #region 数据查询：ExecuteScalar
        /// <summary>
        /// 无参数查询，直接SQL语句
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlString)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// 带参数查询，默认参数属性
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlString, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
        /// 带参数带操作类型的查询，默认参数属性
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlString, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 数据查询：ExecuteDataReader （存储过程）
        /// <summary>
        /// 1.无参数查询，返回DataReader，直接SQL语句（注意关闭）
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReader(string sqlString)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
        /// 2.带参数查询，返回DataReader（注意关闭）
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReader(string sqlString, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
        /// 3.无参数带操作查询，返回DataReader（注意关闭）
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReader(string sqlTypeString, CommandType cmdType)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
        /// 4.带参数带操作查询，返回DataReader（注意关闭）
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReader(string sqlTypeString, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

        #region 数据查询：ExecuteDataSet
        /// <summary>
        /// 执行存储过程带参数, 返回DataSet(含多个DataTable)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">参数名</param>
        /// <param name="DtNameList">返回的DataSet中DataTable的名称列表</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string procName, SqlParameter[] parameters, string[] DtNameList)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(procName, Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (DtNameList != null && DtNameList.Length == ds.Tables.Count)
                    {
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            ds.Tables[i].TableName = (DtNameList[i] == "" || DtNameList[i] == null ? Guid.NewGuid().ToString("N").Replace("-", "").ToUpper() : DtNameList[i]);
                        }
                    }

                    return ds;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }

        public static DataSet ExecuteDataSet(string procName, SqlParameter[] parameters)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(procName, Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }
        public static DataSet ExecuteDataSet(string sqlText)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(sqlText, Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

        #region 数据查询：ExecuteDataTable （存储过程），TODO加入表名参数
        /// <summary>
        /// 1.无参数查询，返回DataTable
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlString)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    //da.Fill(ds, "TableName");
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                dt = null;
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 2.带参数查询，返回DataTable
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlString, params SqlParameter[] parameters)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(sqlString, Connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                dt = null;
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 3.无参数带操作查询，返回DataTable
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlTypeString, CommandType cmdType)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                dt = null;
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 4.带参数带操作查询，返回DataTable
        /// </summary>
        /// <param name="sqlTypeString">SQL语句或者存储过程名</param>
        /// <param name="cmdType">Text/TableDirect/StoredProcedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlTypeString, CommandType cmdType, params SqlParameter[] parameters)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                dt = null;
                throw ex;
            }
            return dt;
        }

        #endregion

        #region 数据查询：CheckIsExist， 检查是否存在相应记录
        /// <summary>
        /// 1.带参数查询，返回bool，直接SQL语句
        /// </summary>
        /// <param name="sqlTypeString"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool CheckIsExist(string sqlTypeString, CommandType cmdType, params SqlParameter[] parameters)
        {
            bool flag = false;
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTypeString, Connection))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        flag = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return flag;
        }
        /// <summary>
        /// 2.无参数查询，返回bool，直接SQL语句
        /// </summary>
        /// <param name="slqString"></param>
        /// <returns></returns>
        public static bool CheckIsExist(string slqString)
        {
            bool flag = false;
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand(slqString, Connection))
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        flag = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                flag = false;
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return flag;
        }

        #endregion

        #region 数据操作：执行多条语句
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务操作
        /// </summary>
        /// <param name="sqlStringList"></param>
        public static void ExecuteSqlTransaction(ArrayList sqlStringList)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            SqlTransaction ts = Connection.BeginTransaction();
            cmd.Transaction = ts;
            try
            {
                for (int n = 0; n < sqlStringList.Count; n++)
                {
                    string strSql = sqlStringList[n].ToString();
                    if (strSql.Trim().Length > 1)
                    {
                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                    }
                }
                ts.Commit();
            }
            catch (SqlException ex)
            {
                ts.Rollback();
                throw ex;
            }
        }
        #endregion

        #region 数据操作：ExecuteXmlReader
        /// <summary>
        /// 1.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// 2.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, (SqlTransaction)null, commandType, commandText, parameters);
            XmlReader retval = cmd.ExecuteXmlReader();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// 3.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(string spName, params object[] paramValues)
        {
            if ((paramValues != null) && (paramValues.Length > 0))
            {
                SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(Connection.ConnectionString, spName);
                AssignParameterValues(parameters, paramValues);
                return ExecuteXmlReader(CommandType.StoredProcedure, spName, parameters);
            }
            else
            {
                return ExecuteXmlReader(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// 4.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// 5.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction, commandType, commandText, parameters);
            XmlReader retval = cmd.ExecuteXmlReader();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// 6.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="spName"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] paramValues)
        {
            if ((paramValues != null) && (paramValues.Length > 0))
            {
                SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(parameters, paramValues);
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, parameters);
            }
            else
            {
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion
        #region  带参数的存储过程,参数以string[] paramList = { "name~type~Direction~size~value", "name~type~Direction~size~value" }

        private static SqlParameter[] CreatePrams(string[] PramsName, SqlDbType[] DbType, ParameterDirection[] _dire, int[] _size, string[] _value)
        {
            SqlParameter[] Prams = new SqlParameter[PramsName.Length];
            for (int i = 0, j = PramsName.Length; i < j; i++)
            {
                Prams[i] = new SqlParameter(PramsName[i], DbType[i], _size[i]);
                Prams[i].Direction = _dire[i];

                if (_dire[i] == ParameterDirection.Input)
                {
                    Prams[i].Value = _value[i];
                }
            }
            return Prams;
        }
        /// <summary>
        /// 设置存储过程参数
        /// </summary>
        /// <param name="paramList">string[] paramList = { "name~type~Direction~size~value", "name~type~Direction~size~value" };</param>
        /// <returns></returns>
        public static SqlParameter[] QucikCreatePrams(string[] paramList)
        {
            string[] paramList_Name = new string[paramList.Length];

            SqlDbType[] paramList_DbType = new SqlDbType[paramList.Length];

            int[] paramList_Size = new int[paramList.Length];

            string[] paramList_Values = new string[paramList.Length];

            ParameterDirection[] paramList_Dire = new ParameterDirection[paramList.Length];

            for (int i = 0; i < paramList.Length; i++)
            {
                paramList_Name[i] = paramList[i].Split('~')[0];

                paramList_DbType[i] = (SqlDbType)Enum.Parse(typeof(SqlDbType), paramList[i].Split('~')[1]);

                paramList_Dire[i] = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), paramList[i].Split('~')[2]);

                paramList_Size[i] = int.Parse(paramList[i].Split('~')[3]);

                paramList_Values[i] = paramList[i].Split('~')[4];
            }
            SqlParameter[] Prams = CreatePrams(paramList_Name, paramList_DbType, paramList_Dire, paramList_Size, paramList_Values);
            return Prams;
        }


        /// <summary>
        /// 对变量属性组成字符串
        /// </summary>
        /// <param name="_name">变量名称</param>
        /// <param name="_type">变量类型</param>
        /// <param name="_Direction">变量变量方向</param>
        /// <param name="_size">变量长度</param>
        /// <param name="_value">变量值</param>
        /// <returns></returns>
        public static string getarrayst(string _name, string _type, string _Direction, string _size, string _value)
        {
            return String.Format("{0}~{1}~{2}~{3}~{4}", _name, _type, _Direction, _size, _value);
        }
        #endregion

        #region 执行存储过程并返回ouput列表
        /// <summary>
        /// 执行存储过程返回对应的ouput列表
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">入参列表</param>        
        /// <returns></returns>
        public static Hashtable ExecuteProc(string procName, SqlParameter[] parameters)
        {
            Hashtable OutPutList = new Hashtable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Connection;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        //if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.ReturnValue)
                        if (param.Direction == ParameterDirection.Output)
                        {
                            OutPutList.Add(param.ParameterName, param.Value);
                        }
                    }
                    return OutPutList;
                }
            }
            catch (SqlException ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

        #region datatable更新数据库
        /// <summary>
        /// datatable更新数据库
        /// </summary>
        /// <param name="_dt">需要更新的DataTable</param>
        /// <param name="TableName">对应的数据库表</param>
        public static void Datatabletosqlserver(DataTable _dt, string TableName)
        {
            try
            {
                string columnList = string.Empty;
                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    columnList += _dt.Columns[i].ColumnName.ToString().Trim() + ",";
                }
                columnList = columnList.TrimEnd(',');
                string SelectQuery = String.Format("select top 0 {0} from {1}", columnList, TableName);
                //SqlDataAdapter datp = new SqlDataAdapter(SelectQuery, Connection);
                SqlDataAdapter datp = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(SelectQuery, Connection);
                datp.SelectCommand = cmd;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(datp);
                datp.Fill(_dt);
                datp.Update(_dt);
            }
            catch (System.Exception ex)
            {
                //Logger.DefaultLog(ex.Message + ex.StackTrace, LogType.Error);
                throw (ex);
            }
        }
        #endregion

        #region 带返回值的事务存储过程
        public static Hashtable ExecuteProc_EX(string procName, SqlParameter[] parameters)
        {
            Hashtable OutPutList = new Hashtable();
            SqlTransaction ts = Connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Transaction = ts;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Connection;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        //if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.ReturnValue)
                        if (param.Direction == ParameterDirection.Output)
                        {
                            OutPutList.Add(param.ParameterName, param.Value);
                        }
                    }
                    if (OutPutList.Contains("@istrue") & OutPutList["@istrue"].ToString().ToUpper() == "F")
                    {
                        ts.Rollback();
                    }
                    else
                    {
                        ts.Commit();
                    }

                    return OutPutList;
                }
            }
            catch (SqlException ex)
            {
                ts.Rollback();
                return null;
                throw ex;
            }
        }
        #endregion
    }
}

/// <summary>
/// 来自M$的SqlHelperParameterCache
/// 支持函数来实现静态缓存存储过程参数，并支持在运行时得到存储过程的参数
/// </summary>
public sealed class SqlHelperParameterCache
{
    #region private methods, variables, and constructors

    //类提供的都是静态方法，将默认构造函数设置为私有的以便阻止利用"new MSSqlHelperParameterCache()"来实例化类
    private SqlHelperParameterCache() { }

    //存储过程参数缓存导HashTable中
    private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

    /// <summary>
    /// resolve at run time the appropriate set of SqlParameters for a stored procedure
    /// 在运行时得到一个存储过程的一系列参数信息
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="connectionString">一个连接对象的有效连接串</param>
    /// <param name="spName">the name of the stored procedure</param>
    /// <param name="spName">存储过程名</param>
    /// <param name="includeReturnValueParameter">是否有返回值参数</param>
    /// <returns>参数对象数组，存储过程的所有参数信息</returns>
    private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
    {
        using (SqlConnection cn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, cn))
        {
            cn.Open();
            cmd.CommandType = CommandType.StoredProcedure;

            //从 SqlCommand 指定的存储过程中检索参数信息，并填充指定的 SqlCommand 对象的 Parameters 集。
            SqlCommandBuilder.DeriveParameters(cmd);

            if (!includeReturnValueParameter)
            {
                //移除第一个参数对象，因为没有返回值，而默认情况下，第一个参数对象是返回值
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count]; ;

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            return discoveredParameters;
        }
    }

    //复制缓存SqlParameter参数数组（深克隆）
    private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
    {
        SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

        for (int i = 0, j = originalParameters.Length; i < j; i++)
        {
            clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
        }

        return clonedParameters;
    }

    #endregion

    #region caching functions

    /// <summary>
    /// 将参数数组添加到缓存中
    /// </summary>
    /// <param name="connectionString">有效的连接串</param>
    /// <param name="commandText">一个存储过程名或者T-SQL命令</param>
    /// <param name="commandParameters">一个要被缓存的参数对象数组</param>
    public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
    {
        string hashKey = connectionString + ":" + commandText;

        paramCache[hashKey] = commandParameters;
    }

    /// <summary>
    /// 从缓存中获得参数对象数组
    /// </summary>
    /// <param name="connectionString">有效的连接串</param>
    /// <param name="commandText">一个存储过程名或者T-SQL命令</param>
    /// <returns>一个参数对象数组</returns>
    public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
    {
        string hashKey = connectionString + ":" + commandText;

        SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];

        if (cachedParameters == null)
        {
            return null;
        }
        else
        {
            return CloneParameters(cachedParameters);
        }
    }

    #endregion caching functions

    #region Parameter Discovery Functions

    /// <summary>
    /// 获得存储过程的参数集
    /// </summary>
    /// <remarks>
    /// 这个方法从数据库中获得信息，并将之存储在缓存，以便之后的使用
    /// </remarks>
    /// <param name="connectionString">有效的连接串</param>
    /// <param name="commandText">一个存储过程名或者T-SQL命令</param>
    /// <returns>一个参数对象数组</returns>
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
    {
        return GetSpParameterSet(connectionString, spName, false);
    }

    /// <summary>
    /// 获得存储过程的参数集
    /// </summary>
    /// <remarks>
    /// 这个方法从数据库中获得信息，并将之存储在缓存，以便之后的使用
    /// </remarks>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="spName">the name of the stored procedure</param>
    /// <param name="includeReturnValueParameter">a bool value indicating whether the return value parameter should be included in the results</param>
    /// <returns>an array of SqlParameters</returns>
    /// <param name="connectionString">有效的连接串</param>
    /// <param name="commandText">一个存储过程名</param>
    /// /// <param name="includeReturnValueParameter">是否有返回值参数</param>
    /// <returns>一个参数对象数组</returns>
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
    {
        string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

        SqlParameter[] cachedParameters;

        cachedParameters = (SqlParameter[])paramCache[hashKey];

        if (cachedParameters == null)
        {
            cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
        }

        return CloneParameters(cachedParameters);
    }

    #endregion Parameter Discovery Functions

}
