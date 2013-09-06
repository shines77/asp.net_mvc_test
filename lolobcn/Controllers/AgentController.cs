using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lolobcn.Controllers
{
    public class ViewAgentController : Controller
    {
        public ViewResult GetView()
        {
            return View();
        }

        public ViewResult GetView(object model)
        {
            return View(model);
        }

        public ViewResult GetView(string viewName)
        {
            return View(viewName);
        }

        public ViewResult GetView(string viewName, object model)
        {
            return View(viewName, model);
        }

        public ViewResult GetView(IView view, object model)
        {
            return View(view, model);
        }

        public ViewResult GetView(string viewName, string masterName, object model)
        {
            return View(viewName, masterName, model);
        }
    }
}
