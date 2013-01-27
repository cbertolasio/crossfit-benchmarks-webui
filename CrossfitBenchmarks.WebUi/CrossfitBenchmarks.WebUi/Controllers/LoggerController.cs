using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Models.Logger;
using CrossfitBenchmarks.WebUi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CrossFitTools.Web.CustomActionResults;
using AutoMapper;


namespace CrossFitTools.Web.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ICrossfitBenchmarksServices webServiceApi;
        
        [HttpPost]
        public ActionResult AddLogEntry(AddLogEntryViewModel dataToSave)
        {
            var dto = Mapper.Map<LogEntryDto>(dataToSave);
            var result = webServiceApi.CreateLogEntry(dto);
            return new CustomJsonResult { Data = result };
        }

        public ActionResult Index(string val)
        {
            switch (val)
            {
                case "benchmark":
                    var result = webServiceApi.GetTheBenchmarks("3");
                    var listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<CrossfitBenchmarks.WebUi.Models.Logger.BenchmarkItemViewModel>>(result);
                    var viewModel = new CrossfitBenchmarks.WebUi.Models.Logger.BenchmarksViewModel { Benchmarks = listItems.ToList() };
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