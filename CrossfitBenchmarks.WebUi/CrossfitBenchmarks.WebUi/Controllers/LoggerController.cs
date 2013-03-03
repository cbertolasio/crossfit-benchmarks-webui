using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Models.Logger;
using CrossfitBenchmarks.WebUi.Services;
using CrossfitBenchmarks.WebUi.Utility;
using CrossFitTools.Web.CustomActionResults;
using CrossfitBenchmarks.WebUi.Extensions;
using System.Security.Claims;

namespace CrossFitTools.Web.Controllers
{
    [Authorize]
    public class LoggerController : Controller
    {
        private readonly IOpenGraphServices openGraph;
        private readonly ICrossfitBenchmarksServices webServiceApi;
        [HttpPost]
        [NoCache]
        public ActionResult AddLogEntry(AddLogEntryViewModel dataToSave)
        {
            dataToSave.DateCreated = DateTimeHelper.Combine(dataToSave.DateCreated, dataToSave.TimeCreated);
            var dto = Mapper.Map<LogEntryDto>(dataToSave);
            
            var result = webServiceApi.CreateLogEntry(dto);

            openGraph.PublishAction(dto, User.Identity);

            var updatedViewModel = Mapper.Map<WorkoutLogEntryDto, WodItemViewModel>(result);
            return new CustomJsonResult { Data = updatedViewModel };
        }

        public ActionResult Index(string val)
        {
            IEnumerable<WorkoutLogEntryDto> result = null;
            IEnumerable<WodItemViewModel> listItems = null;
            switch (val)
            {
                case "benchmark":
                    result = webServiceApi.GetTheBenchmarks();
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var viewModel = new BenchmarksViewModel { WodList = listItems.ToList() };
                    return View("Benchmarks", viewModel );

                case "thegirls":
                    result = webServiceApi.GetTheGirls();
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var theGirlsViewModel = new TheGirlsViewModel { WodList = listItems.ToList() };
                    return View("TheGirls", theGirlsViewModel);

                case "theheros":
                    result = webServiceApi.GetTheHeros();
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var theHerosViewModel = new TheHerosViewModel { WodList = listItems.ToList() };
                    return View("TheHeros", theHerosViewModel);
                    break;
                default:
                    return new EmptyResult();
            }
        }

        public LoggerController(ICrossfitBenchmarksServices webServiceApi, IOpenGraphServices openGraph)
        {
            this.openGraph = openGraph;
            this.webServiceApi = webServiceApi;

        }
    }
}