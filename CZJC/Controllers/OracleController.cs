using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZJC.Controllers
{
    public class OracleController : Controller
    {
        // GET: Oracle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report()
        {
            ViewBag.总连接数 = "120";
            ViewBag.已用连接 = 60;
            ViewBag.可用连接 = 60;
            return View();
        }
    }
}