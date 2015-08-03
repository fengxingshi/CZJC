using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace CZOA.WebAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public string Index()
        {
            //ViewBag.Title = "Home Page";
            return "you kid me?";
        }
    }
}
