using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using lolobcn.Models;
using System.Reflection;

//using System.Data.DataSetExtensions;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace lolobcn.Controllers
{
    public class HomeController : ViewAgentController
    {
        //public GridView GridView1 = new GridView();

        private lolobcn.Models.LolObDBModelEntities _db2 = new Models.LolObDBModelEntities();
        //private lolobcn.Models.LolObDBModels _db = new Models.LolObDBModels();
        private lolobcn.Models.LolObDBModelsEx2 _db = new Models.LolObDBModelsEx2();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult MatchList()
        {
            /***** http://www.cnblogs.com/zhanqi/archive/2011/01/06/1929256.html *****/
            /*
            string connStr = "server=localhost; port=3306; database=lolobcn_net; uid=root; pwd=gxh201100;";
            //List<DataRow> list = null;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter("select * from matchinfo", conn);
                da.Fill(dt);
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                //list = dt.AsEnumerable().ToList();
                var items = dt.ToList<Depends.MatchinfoTable>();

                //List<Depends.MatchinfoTable> list = new List<Depends.MatchinfoTable>(dt.Select());

                conn.Close();
            }
            //*/
            ///*
            //string strError;
            //DataTable dt = _db.GetDataTable("select * from matchinfo", out strError);
            //var list = dt.ToList<Depends.MatchinfoTable>();
            //var list = _db.GetDataToList<MatchinfoTable>("select * from matchinfo");
            //return (list != null) ? View(list) : View();
            //
            //return _db.GetView<MatchinfoTable>("select * from matchinfo", this);
            return _db.GetView<MatchinfoTable2>(this);
            //*/
            //return View(_db.Matchinfo.ToList());
        }

        public ActionResult About()
        {
            //viewResult = View(_db2.Matchinfo.ToList());
            return _db.GetView<MatchinfoTable2>(this);
            //return View(_db2.Matchinfo.ToList());
            //return View();
        }
    }
}
