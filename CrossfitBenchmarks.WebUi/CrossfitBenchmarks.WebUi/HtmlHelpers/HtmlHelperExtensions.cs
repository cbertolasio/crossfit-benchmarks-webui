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

    using System.Web.Mvc.Html;
    using System.Web.Routing;

    /// <summary>
    /// use @Html.ToJson(Model) output json as a string into the dom
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li") {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString()
            };

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

        
    }
}
