﻿using System;
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
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-logo-x128.jpg";
            parameters["score"] = "2:30";
            parameters["everywod:score"] = "2:30";


 //<head prefix="og: http://ogp.me/ns# fb: http://ogp.me/ns/fb# everywod: http://ogp.me/ns/fb/everywod#">
 // <meta property="fb:app_id"      content="460497347351482" /> 
 // <meta property="og:type"        content="everywod:personal_record" /> 
 // <meta property="og:url"         content="Put your own URL to the object here" /> 
 // <meta property="og:title"       content="Sample Personal Record" /> 
 // <meta property="og:image"       content="https://fbstatic-a.akamaihd.net/images/devsite/attachment_blank.png" /> 
 // <meta property="everywod:score" content="Sample score" /> 

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
