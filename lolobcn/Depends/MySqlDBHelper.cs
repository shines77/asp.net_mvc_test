using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Diagnostics;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace lolobcn.Depends
{
    public partial class MySqlDBHelper
    {
        private string dbConnString = "";
        private string dbConnStringName = "";

        public enum ConnectionMode
        {
            BY_STRING = 0,
            BY_NAME
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

        public void SetConnStringInfo(string connString, ConnectionMode connMode = ConnectionMode.BY_NAME)
        {
            InitConnStringInfo(connString, connMode);
        }

        private void InitConnStringInfo(string connString, ConnectionMode connMode = ConnectionMode.BY_NAME)
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

        private string GetSystemConfigConnectionStrings(string connectionStringName)
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
    }
}
