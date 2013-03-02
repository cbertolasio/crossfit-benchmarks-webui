using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Extensions;
using CrossfitBenchmarks.WebUi.Models.Admin;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;
using RestSharp.Serializers;
using System.Dynamic;
using System.Text.RegularExpressions;

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

        public ActionResult ViewClaims()
        {
            ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var nameIdentifierClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ViewBag.NameIdentifierValue = nameIdentifierClaim.Value;
            ViewBag.IdentityProviderValue = claimsIdentity.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value;

            return View();
        }

        public ActionResult FbTestClient()
        {

            ViewBag.AccessToken = GetAccessToken();
            return View();
        }

        private string GetAccessToken() {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst("http://www.facebook.com/claims/AccessToken").Value;
        }

        public ActionResult PostPr() {
            var accessToken = GetAccessToken();
            var client = new Facebook.FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = accessToken;
            parameters["personal_record"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/personalrecord";
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:type"] = "everywod:personal_record";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/personalrecord";
            parameters["og:title"] = "Personal Record";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            parameters["score"] = "2:30";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            ViewBag.PostedResult = result;
            return View("FbTestClient");
        }

        public ActionResult PostBenchmark()
        {
            var accessToken = GetAccessToken();
            var client = new Facebook.FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = accessToken;
            parameters["benchmark"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/benchmark";
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:type"] = "everywod:benchmark";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/benchmark";
            parameters["og:title"] = "Benchmark";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            parameters["score"] = "220#";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            ViewBag.PostedResult = result;
            return View("FbTestClient");

        }

        public ActionResult PostABasicWod()
        {
            var accessToken = GetAccessToken();
            var client = new Facebook.FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = accessToken;
            parameters["basic_workout"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/basicworkout";
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:type"] = "everywod:basic_workout";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/basicworkout";
            parameters["og:title"] = "Basic WOD";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-logo-x128.jpg";
            parameters["score"] = "220";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            ViewBag.PostedResult = result;
            return View("FbTestClient");

        }

        public ActionResult PostAGirl()
        {
            var accessToken = GetAccessToken();
            var client = new Facebook.FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = accessToken;
            parameters["girl_workout"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/girlworkout";
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:type"] = "everywod:girls";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/girlworkout";
            parameters["og:title"] = "Basic WOD";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            parameters["score"] = "220";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            ViewBag.PostedResult = result;
            return View("FbTestClient");

        }

        public ActionResult PostAHero()
        {
            var accessToken = GetAccessToken();
            var client = new Facebook.FacebookClient(accessToken);
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = accessToken;
            parameters["hero_workout"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/heroworkout";
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:type"] = "everywod:hero_workout";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/heroworkout";
            parameters["og:title"] = "Basic WOD";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            parameters["score"] = "220";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            ViewBag.PostedResult = result;
            return View("FbTestClient");

        }



        public AdminController(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }
    }
}
