using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CrossfitBenchmarks.WebUi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CrossfitBenchmarks.WebUi.Utility;
using CrossFitTools.Web.CustomActionResults;

namespace CrossfitBenchmarks.WebUi.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        public  WorkoutController()
        {
        }

        public ActionResult History()
        {
            return View("History");
        }
    }
}

