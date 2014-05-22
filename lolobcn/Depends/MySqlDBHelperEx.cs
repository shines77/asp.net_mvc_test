using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Web.Mvc;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.Objects.DataClasses;
using System.Threading;
using MySql.Data.MySqlClient;

using lolobcn.Controllers;

namespace lolobcn.Depends
{
    /// <summary>
    /// 
    /// MySqlDBHelperEx:
    ///     增强型的MySqlDBHelper类, 允许你手动控制数据库的打开和关闭,
    ///     在需要多次存取同一个数据库的不同表格或多次数据库操作时, 可以提高效率.
    /// 
    /// </summary>
    public partial class MySqlDBHelperEx : MySqlDBHelper
    {
        protected MySqlConnection sqlConn = null;

        public MySqlDBHelperEx()
        {
        }

        public MySqlDBHelperEx(string connStringName)
        {
            InitConnStringInfo(connStringName);
        }

        public MySqlDBHelperEx(string connString, ConnectionMode connMode)
        {
            InitConnStringInfo(connString, connMode);
        }

        public bool Open()
        {
            if (sqlConn == null)
                sqlConn = new MySqlConnection(dbConnString);

            if (sqlConn != null)
            {
                ConnectionState state = sqlConn.State;

                if (state == ConnectionState.Open)
                    return true;

                if (state == ConnectionState.Closed)
                {
                    sqlConn.Open();
                    return (sqlConn.State == ConnectionState.Open);
                }

                // 如果mysql还在处理数据中, 等待一下直到处于open或close状态为止, 事实上可能这些状态并未实现, 为将来的版本准备
                while (state != ConnectionState.Broken
                    && state != ConnectionState.Closed && state != ConnectionState.Open)
                {
                    Thread.Sleep(1);
                    state = sqlConn.State;
                }

                if (state == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                else if (state == ConnectionState.Broken)
                {
                    sqlConn.Clone();
                    sqlConn.Open();
                }
                return (sqlConn.State == ConnectionState.Open);
            }
            else
                return false;
        }

        public void Close()
        {
            if (sqlConn != null)
            {
                sqlConn.Close();
                sqlConn = null;
            }
        }

        public DataTable GetDataTable(string strSQL, out string strError, bool bAutoClose = true)
        {
            DataTable dt = null;
            strError = string.Empty;
            try
            {
                if (Open())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, sqlConn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        cmd.Parameters.Clear();
                    }
                    // 这里为什么没用try{}...finally{}...来关闭, 我认为差不多, 这样写逻辑更顺畅一点
                    if (bAutoClose)
                        Close();
                }
            }
            catch (MySqlException ex)
            {
                strError = ex.Message;
                Trace.WriteLine(ex);

                if (bAutoClose)
                    Close();
            }
            return dt;
        }

        public DataSet GetDataSet(string strSQL, out string strError, bool bAutoClose = true)
        {
            DataSet ds = null;
            strError = string.Empty;
            try
            {
                if (Open())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, sqlConn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        ds = new DataSet();
                        adapter.Fill(ds);
                        cmd.Parameters.Clear();
                    }
                    if (bAutoClose)
                        Close();
                }
            }
            catch (MySqlException ex)
            {
                strError = ex.Message;
                Trace.WriteLine(ex);

                if (bAutoClose)
                    Close();
            }
            return ds;
        }

        public IList<T> GetDataToList<T>(string strSQL, bool bAutoClose = true) where T : new()
        {
            string strError;
            DataTable dt = GetDataTable(strSQL, out strError, bAutoClose);
            var list = dt.ToList<T>();
            return list;
        }

        public ViewResult GetView<T>(string strSQL, ViewAgentController controller, bool bAutoClose = true) where T : new()
        {
            string strError;
            DataTable dt = GetDataTable(strSQL, out strError, bAutoClose);
            var list = dt.ToList<T>();
            return (list != null) ? controller.GetView(list) : controller.GetView();
        }

        public ViewResult GetView<T>(ViewAgentController controller, bool bAutoClose = true) where T : new()
        {
            T obj = new T();
            FieldInfo fi = typeof(T).GetField("strSQL");
            // 为了利用反射获取返回Field值，必须指定BindingFlags.Instance 或 BindingFlags.Static
            if (fi != null && fi.IsStatic && fi.IsPublic)
            {
                string strSQL = (string)fi.GetValue(obj);
                string strError;
                DataTable dt = GetDataTable(strSQL, out strError, bAutoClose);
                var list = dt.ToList<T>();
                return (list != null) ? controller.GetView(list) : controller.GetView();
            }
            return controller.GetView();
        }

        public ViewResult GetView<T>(ViewAgentController controller, IView view, bool bAutoClose = true) where T : new()
        {
            T obj = new T();
            FieldInfo fi = typeof(T).GetField("strSQL");
            // 为了利用反射获取返回Field值，必须指定BindingFlags.Instance 或 BindingFlags.Static
            if (fi != null && fi.IsStatic && fi.IsPublic)
            {
                string strSQL = (string)fi.GetValue(obj);
                string strError;
                DataTable dt = GetDataTable(strSQL, out strError, bAutoClose);
                var list = dt.ToList<T>();
                return (list != null) ? controller.GetView(view, list) : controller.GetView(view);
            }
            return controller.GetView(view);
        }
    }
}
