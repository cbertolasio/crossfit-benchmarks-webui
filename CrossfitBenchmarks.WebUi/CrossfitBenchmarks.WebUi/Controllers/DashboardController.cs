using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrossfitBenchmarks.WebUi.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Settings(){
            return View();
        }

        public ActionResult Profile(){
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
