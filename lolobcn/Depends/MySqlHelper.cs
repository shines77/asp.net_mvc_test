using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace lolobcn.Depends
{
    public class MySqlHelper
    {
        public static string ConnString = "Server=localhost;Port=3306;Database=lolobcn_net;Uid=root;Pwd=gxh201100;pooling=false;";

        public static string Conn_Config_Str_Name = string.Empty;

        public static string Conn_Server = string.Empty;
        public static string Conn_DBName = string.Empty;
        public static string Conn_Uid = string.Empty;
        public static string Conn_Pwd = string.Empty;

        private static string _ConnString
        {
            get
            {
                if (!string.IsNullOrEmpty(ConnString))
                    return ConnString;

                object oConn = ConfigurationManager.ConnectionStrings[Conn_Config_Str_Name];
                if (oConn != null && oConn.ToString() != "")
                    return oConn.ToString();

                return string.Format(@"server={0};database={1};userid={2};password={3}", Conn_Server, Conn_DBName, Conn_Uid, Conn_Pwd);
            }
        }

        // 读取数据 datatable
        public static DataTable GetDataTable(out string sError, string sSQL)
        {
            DataTable dt = null;
            sError = string.Empty;

            MySqlConnection myConn = null;
            try
            {
                myConn = new MySqlConnection(_ConnString);
                MySqlCommand myCommand = new MySqlCommand(sSQL, myConn);
                myConn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(myCommand);
                dt = new DataTable();
                adapter.Fill(dt);
                myConn.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return dt;
        }

        // 读取数据 dataset
        public static DataSet GetDataSet(out string sError, string sSQL)
        {
            DataSet ds = null;
            sError = string.Empty;

            MySqlConnection myConn = null;
            try
            {
                myConn = new MySqlConnection(_ConnString);
                MySqlCommand myCmd = new MySqlCommand(sSQL, myConn);
                myConn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(myCmd);
                ds = new DataSet();
                adapter.Fill(ds);
                myConn.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return ds;
        }

        // 取最大的ID
        public static Int32 GetMaxID(out string sError, string sKeyField, string sTableName)
        {
            DataTable dt = GetDataTable(out sError, "select IFNULL(max(" + sKeyField + "),0) as MaxID from " + sTableName);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return 0;
        }

        // 插入，修改，删除，是否使用事务
        public static bool UpdateData(out string sError, string sSQL, bool bUseTransaction = false)
        {
            int iResult = 0;
            sError = string.Empty;

            MySqlConnection myConn = null;

            if (!bUseTransaction)
            {
                try
                {
                    myConn = new MySqlConnection(_ConnString);
                    MySqlCommand myCmd = new MySqlCommand(sSQL, myConn);
                    myConn.Open();
                    iResult = myCmd.ExecuteNonQuery();
                    myConn.Close();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                }
            }
            else // 使用事务
            {
                MySqlTransaction myTrans = null;
                try
                {
                    myConn = new MySqlConnection(_ConnString);
                    myConn.Open();
                    myTrans = myConn.BeginTransaction();
                    MySqlCommand myCmd = new MySqlCommand(sSQL, myConn);
                    myCmd.Transaction = myTrans;
                    iResult = myCmd.ExecuteNonQuery();
                    myTrans.Commit();
                    myConn.Close();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                    myTrans.Rollback();
                }
            }

            return iResult > 0;
        }
    }
}