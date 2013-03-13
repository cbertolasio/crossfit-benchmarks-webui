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

namespace CrossFitTools.Web.CustomActionResults
{
    public class CustomJsonResult : JsonResult
    {
        public CustomJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                    String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JSON GET NOT ALLOWED -  Check the JsonRequestBehavior");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                StringBuilder text = new StringBuilder();
#if DEBUG
                const Formatting formatting = Formatting.Indented;
#else
        			const Formatting formatting = Formatting.None;
#endif
                JsonSerializer jsonSerializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = formatting };
                jsonSerializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                jsonSerializer.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

                using (StringWriter stringWriter = new StringWriter(text, CultureInfo.InvariantCulture))
                {
                    jsonSerializer.Serialize(stringWriter, Data);
                    response.Write(text.ToString());
                }
            }
        }
    }
}

