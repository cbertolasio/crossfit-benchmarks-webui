using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrossfitBenchmarks.WebUi.Controllers
{
    public class FacebookObjectsController : Controller
    {
        
        public ActionResult PersonalRecord()
        {
            return View();
        }

        public ActionResult Benchmark() {
            return View();
        }

        public ActionResult BasicWorkout() {
            return View();
        }

        public ActionResult GirlWorkout() {
            return View();
        }

        public ActionResult HeroWorkout() {
            return View();
        }

    }
}
