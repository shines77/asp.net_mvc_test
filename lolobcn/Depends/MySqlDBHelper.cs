using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Data.Objects.DataClasses;
using MySql.Data.MySqlClient;

using lolobcn.Controllers;

namespace lolobcn.Depends
{
    /// <summary>
    /// 
    /// MySqlDBHelper: 连接mysql的帮助类, 协助完成连接mysql的各种操作
    /// 
    /// 注: Partial 表示局部类型, 局部类型是一个纯语言层的编译处理，不影响任何执行机制,
    ///     它允许我们将一个类、结构或接口分成几个部分，分别实现在几个不同的.cs文件中.
    /// </summary>
    public partial class MySqlDBHelper
    {
        protected string dbConnString = "";
        protected string dbConnStringName = "";

        public enum ConnectionMode
        {
            BY_NAME = 0,
            BY_STRING,
        };

        public MySqlDBHelper()
        {
        }

        public MySqlDBHelper(string connStringName)
        {
            InitConnStringInfo(connStringName);
        }

        public MySqlDBHelper(string connString, ConnectionMode connMode)
        {
            InitConnStringInfo(connString, connMode);
        }

        public string ConnString
        {
            get { return dbConnString; }
            set
            {
                InitConnStringInfo(value, ConnectionMode.BY_NAME);
            }
        }

        public void SetConnStringInfo(string connString, ConnectionMode connMode = ConnectionMode.BY_NAME)
        {
            InitConnStringInfo(connString, connMode);
        }

        protected void InitConnStringInfo(string connString, ConnectionMode connMode = ConnectionMode.BY_NAME)
        {
            if (connString == null)
                return;

            if (connMode == ConnectionMode.BY_STRING)
            {
                dbConnString = connString.Trim();
            }
            else if (connMode == ConnectionMode.BY_NAME)
            {
                int nStartIndex = 0, nCursorIndex = 0;
                int nToken = 0;
                const string strToken = "=";
                while ((nStartIndex = connString.IndexOf(strToken, nStartIndex)) != -1)
                {
                    nToken++;
                    nCursorIndex = nStartIndex;
                    nStartIndex += strToken.Length;
                    if (nToken > 1)
                        break;
                }
                if (nToken == 1 && nCursorIndex > 0)
                {
                    string strPrefix = connString.Substring(0, nCursorIndex).Trim().ToLower();
                    if (strPrefix.Equals("name"))
                        dbConnStringName = connString.Substring(nCursorIndex + strToken.Length).Trim();
                    else
                        dbConnStringName = connString.Trim();
                }
                else
                    dbConnStringName = connString.Trim();
                //dbConnString = System.Configuration.ConfigurationManager.ConnectionStrings[dbConnStringName].ToString();
                dbConnString = GetSystemConfigConnectionStrings(dbConnStringName);
            }
        }

        protected string GetSystemConfigConnectionStrings(string connectionStringName)
        {
            string connectionString = null;
            if (!string.IsNullOrEmpty(connectionStringName))
            {
                Object objConn = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
                if (objConn != null)
                    connectionString = objConn.ToString();
            }
            return connectionString;
        }

        public DataTable GetDataTable(string strSQL, out string strError)
        {
            DataTable dt = null;
            strError = string.Empty;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConnString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        conn.Open();

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        cmd.Parameters.Clear();

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                Trace.WriteLine(ex);
            }
            return dt;
        }

        public DataSet GetDataSet(string strSQL, out string strError)
        {
            DataSet ds = null;
            strError = string.Empty;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConnString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        conn.Open();

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        ds = new DataSet();
                        adapter.Fill(ds);
                        cmd.Parameters.Clear();

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                Trace.WriteLine(ex);
            }
            return ds;
        }

        public IList<T> GetDataToList<T>(string strSQL) where T : new()
        {
            string strError;
            DataTable dt = GetDataTable(strSQL, out strError);
            var list = dt.ToList<T>();
            return list;
        }

        public ViewResult GetView<T>(string strSQL, ViewAgentController controller) where T : new()
        {
            string strError;
            DataTable dt = GetDataTable(strSQL, out strError);
            var list = dt.ToList<T>();
            return (list != null) ? controller.GetView(list) : controller.GetView();
        }

        public ViewResult GetView<T>(ViewAgentController controller) where T : new()
        {
            T obj = new T();
            FieldInfo fi = typeof(T).GetField("strSQL");
            // 为了利用反射获取返回Field值，必须指定BindingFlags.Instance 或 BindingFlags.Static
            if (fi != null && fi.IsStatic && fi.IsPublic)
            {
                string strSQL = (string)fi.GetValue(obj);
                //string strSQL = "select * from matchinfo";
                string strError;
                DataTable dt = GetDataTable(strSQL, out strError);
                var list = dt.ToList<T>();
                return (list != null) ? controller.GetView(list) : controller.GetView();
            }
            return controller.GetView();
        }

        /**
         * Throws a new exception with a more informative error message.
         *
         * @param cause
         *            The original exception that will be chained to the new
         *            exception when it's rethrown.
         *
         * @param sql
         *            The query that was executing when the exception happened.
         *
         * @param paramLists
         *            The query replacement parameters; <code>null</code> is a valid
         *            value to pass in.
         *
         * @throw MySqlException
         *             if a database access error occurs
         */
        protected void ReThrow(MySqlExceptionEx cause, string sql, params object[] paramLists)
        {
            string causeMessage = cause.Message;
            if (causeMessage == null) {
                causeMessage = "";
            }
            StringBuilder msg = new StringBuilder(causeMessage);

            msg.Append(" Query: ");
            msg.Append(sql);
            msg.Append(" Parameters: ");

            if (paramLists == null) {
                msg.Append("[]");
            } else {
                msg.Append(paramLists);
            }

            MySqlExceptionEx ex = new MySqlExceptionEx(msg.ToString(), cause.ErrorCode);

            throw ex;
        }
    }
}
