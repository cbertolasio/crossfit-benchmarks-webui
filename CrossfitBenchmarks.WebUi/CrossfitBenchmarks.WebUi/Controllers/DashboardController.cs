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
    public class DashboardController : Controller
    {
        private readonly ICrossfitBenchmarksServices service;
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        [NoCache]
        public ActionResult Index()
        {
            var result = service.GetSummary();
            

            StringBuilder text = new StringBuilder();
#if DEBUG
            const Formatting formatting = Formatting.Indented;
#else
			const Formatting formatting = Formatting.None;
#endif

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = formatting, DateFormatHandling = DateFormatHandling.IsoDateFormat };
            ViewBag.summaryData = Newtonsoft.Json.JsonConvert.DeserializeObject(result, settings);

            return View();
        }

        [NoCache]
        public ActionResult History()
        {
            return new CustomJsonResult { Data = service.GetSummary(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public DashboardController(ICrossfitBenchmarksServices service)
        {
            this.service = service;
        }
    }
}
