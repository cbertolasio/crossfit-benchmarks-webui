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
            IEnumerable<WorkoutLogEntryDto> result = null;
            IEnumerable<WodItemViewModel> listItems = null;
            switch (val)
            {
                case "benchmark":
                    result = webServiceApi.GetTheBenchmarks("3");
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var viewModel = new BenchmarksViewModel { Benchmarks = listItems.ToList() };
                    return View("Benchmarks", viewModel );

                case "thegirls":
                    result = webServiceApi.GetTheGirls("3");
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var theGirlsViewModel = new TheGirlsViewModel { WodList = listItems.ToList() };
                    return View("TheGirls", theGirlsViewModel);

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