using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CrossfitBenchmarks.WebUi.HtmlHelpers
{

    using System.Web;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    /// <summary>
    /// use @Html.ToJson(Model) output json as a string into the dom
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static string IsActiveLink(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var queryString = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString;
            if (controllerName == currentController && actionName == currentAction && routeValues != null && queryString != null)
            {
                var rvd = new RouteValueDictionary(routeValues);
                foreach (var key in queryString.AllKeys)
                {
                    if (queryString[key].Equals(rvd[key]))
                    {
                        return "active";
                        break;
                    }
                }


            }
            else if (controllerName == currentController && actionName == currentAction)
            {
                return "active";
            }

            return string.Empty;
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null, bool isADivider = false)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li") {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString()
            };

            if (isADivider) {
                builder.Attributes["class"] = "divider";
            }
                

            var queryString = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString;
            if (controllerName == currentController && actionName == currentAction && routeValues != null && queryString != null)
            {
                var rvd = new RouteValueDictionary(routeValues);
                foreach (var key in queryString.AllKeys)
                {
                    if (queryString[key].Equals(rvd[key]))
                    {
                        builder.AddCssClass("active");
                        break;
                    }
                }
                

            }
            else if (controllerName == currentController && actionName == currentAction)
            {
                builder.AddCssClass("active");
            }
                

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString ToJson(this HtmlHelper htmlHelper, object data)
        {
            StringBuilder text = new StringBuilder();
#if DEBUG
            const Formatting formatting = Formatting.Indented;
#else
			const Formatting formatting = Formatting.None;
#endif
            JsonSerializer jsonSerializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = formatting };
            jsonSerializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            using (StringWriter stringWriter = new StringWriter(text, CultureInfo.InvariantCulture)) {
                jsonSerializer.Serialize(stringWriter, data);
                return new MvcHtmlString(text.ToString());
            }
        }

        /// <summary>
        /// use @Html.ToJSVar("myVariableName", Model) output a js variable declaration
        /// </summary>
        /// <remarks>
        /// Helper should be used within a <script>block</script>

        /// </remarks>
        /// </remarks>
        public static MvcHtmlString ToJSVar(this HtmlHelper htmlHelper, string varName, object data)
        {
            return new MvcHtmlString("var " + varName + " = " + htmlHelper.ToJson(data) + ";");
        }


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
            if (!uri.IsAbsoluteUri) {
                Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
                UriBuilder builder = new UriBuilder(requestUrl.Scheme, requestUrl.Host, requestUrl.Port);

                builder.Path = VirtualPathUtility.ToAbsolute(path);
                uri = builder.Uri;
            }

            return uri.ToString();
        }
    }
}
