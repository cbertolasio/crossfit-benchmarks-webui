using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CrossfitBenchmarks.WebUi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CrossfitBenchmarks.WebUi.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {

        public ActionResult History(int id)
        {
            var result = webServicesApi.GetWorkoutHistory(id);
            StringBuilder text = new StringBuilder();
#if DEBUG
            const Formatting formatting = Formatting.Indented;
#else
			const Formatting formatting = Formatting.None;
#endif

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = formatting, DateFormatHandling = DateFormatHandling.IsoDateFormat };
            ViewBag.workoutHistoryViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject(result, settings);
            return View("History");
        }

        public WorkoutController(ICrossfitBenchmarksServices webServicesApi)
        {
            this.webServicesApi = webServicesApi;
        }
        private readonly ICrossfitBenchmarksServices webServicesApi;
    }
}

