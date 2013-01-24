using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Services;
using CrossFitTools.Web.Models.Logger;

namespace CrossFitTools.Web.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ICrossfitBenchmarksServices webServiceApi;
        //
        // GET: /Logger/

        public ActionResult Index(string val)
        {
            switch (val)
            {
                case "benchmark":
                    var result = webServiceApi.GetTheBenchmarks("3");
                    var listItems = AutoMapper.Mapper.Map<IEnumerable<WorkoutLogEntryDto>,IEnumerable<BenchmarkItemViewModel>>(result);
                    var viewModel = new BenchmarksViewModel { Benchmarks = listItems.ToList() };
                    return View("Benchmarks", viewModel );

                default:
                    return new EmptyResult();
            }
        }

        public LoggerController(ICrossfitBenchmarksServices webServiceApi)
        {
            this.webServiceApi = webServiceApi;
        }
    }
}
