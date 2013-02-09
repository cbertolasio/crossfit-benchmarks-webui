using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Extensions;
using CrossfitBenchmarks.WebUi.Models.Admin;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;
using RestSharp.Serializers;

namespace CrossfitBenchmarks.WebUi.Controllers
{
     [Authorize]
    public class AdminController : Controller
    {
        private readonly ITokenProvider tokenProvider;
        public ActionResult Index()
        {
            var scope = ConfigurationManager.AppSettings["AcsScope"];

            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("WorkoutTypes", RestSharp.Method.GET);
            request.AddAuthorizationHeader(tokenProvider,  scope);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();

            var response = client.Execute<List<WorkoutTypeDto>>(request);

            var model = new AdminIndexViewModel();
            model.WorkoutTypes = response.Data;
            return View(model);
        }


        public AdminController(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }
    }
}
