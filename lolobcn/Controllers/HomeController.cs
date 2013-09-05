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
using lolobcn.Depends;
using System.Reflection;

//using System.Data.DataSetExtensions;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace lolobcn.Controllers
{
    public class HomeController : Controller
    {
        //public GridView GridView1 = new GridView();

        private lolobcn.Models.LolObDBModelEntities _db = new Models.LolObDBModelEntities();
        //private lolobcn.Depends.LolObDBModels _db = new Depends.LolObDBModels();

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

                conn.Close();
            }
            //*/
            /*
            string strError;
            DataTable dt = _db.GetDataTable("select * from matchinfo", out strError);
            var list = dt.ToList<Depends.MatchinfoTable>();
            return (list != null) ? View(list) : View();
            //*/
            return View(_db.Matchinfo.ToList());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
