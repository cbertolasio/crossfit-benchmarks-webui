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
        private readonly ILogger logger;
        private readonly IOpenGraphServices openGraph;
        private readonly ICrossfitBenchmarksServices webServiceApi;
        [HttpPost]
        [NoCache]
        public ActionResult AddLogEntry(AddLogEntryViewModel dataToSave)
        {
            dataToSave.DateOfWod = DateTimeHelper.Combine(dataToSave.DateOfWod, dataToSave.TimeCreated, dataToSave.ClientTimeZone);
            var dto = Mapper.Map<LogEntryDto>(dataToSave);
            dto.DateOfWodAsString = dto.DateOfWod.ToString();
            var result = webServiceApi.CreateLogEntry(dto);


            try
            {
                openGraph.PublishAction(dto, User.Identity, dataToSave.LogEntryType, dataToSave.IsAPersonalRecord);
            }
            catch (Exception ex)
            {
                var msg = new LogMessage { Message = "Failed to publish an action to facebook...", Category = "Error", AppContext = "FacebookAction", Error = ex.ToString() };
                logger.Log(msg);
            }

            var updatedViewModel = Mapper.Map<WorkoutLogEntryDto, WodItemViewModel>(result);
            updatedViewModel.LastAttemptDateAsString = result.LastEntry.DateOfWod.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
            updatedViewModel.LastPersonalRecordDateAsString = result.LastPersonalRecord.DateOfWod.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");

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

                case "theheroes":
                    result = webServiceApi.GetTheHeroes();
                    listItems = Mapper.Map<IEnumerable<WorkoutLogEntryDto>, IEnumerable<WodItemViewModel>>(result);
                    var theHeroesViewModel = new TheHeroesViewModel { WodList = listItems.ToList() };
                    return View("TheHeroes", theHeroesViewModel);
                default:
                    return new EmptyResult();
            }
        }

        public ActionResult DeleteLogEntry(int id)
        {
            return new CustomJsonResult { Data = new { result = webServiceApi.DeleteLogEntry(id) }};
        }

        public LoggerController(ICrossfitBenchmarksServices webServiceApi, IOpenGraphServices openGraph, ILogger logger)
        {
            this.logger = logger;
            this.openGraph = openGraph;
            this.webServiceApi = webServiceApi;
        }
    }
}