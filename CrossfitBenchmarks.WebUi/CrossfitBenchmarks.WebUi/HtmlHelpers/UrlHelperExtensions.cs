using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace CrossfitBenchmarks.WebUi.HtmlHelpers
{
    public static class UrlHelperExtensions
    {
        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName)
        {
            return url.Action(actionName, controllerName, null, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            return url.Action(actionName, controllerName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string AbsoluteContent(this UrlHelper url, string path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

            //If the URI is not already absolute, rebuild it based on the current request.
            if (!uri.IsAbsoluteUri)
            {
                Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
                UriBuilder builder = new UriBuilder(requestUrl.Scheme, requestUrl.Host, requestUrl.Port);

                builder.Path = VirtualPathUtility.ToAbsolute(path);
                uri = builder.Uri;
            }

            return uri.ToString();
        }
    }
}

