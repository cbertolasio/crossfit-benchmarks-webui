using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public class UnifiedLogger : ILogger
    {
        
        private static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings { Formatting = Formatting.None, 
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat, 
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, 
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind };
        }

        public void LogError(string message, Exception exception)
        {
            var msg = new LogMessage{Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"],  Message = message, Error =  exception.ToString(), Category = "Error" };
            Trace.TraceError(JsonConvert.SerializeObject(msg, GetSettings()));
        }

        public void LogMessage(string message)
        {
            var msg = new LogMessage { Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"], Message = message,Category = "Info" };
            Trace.TraceInformation(message);
        }

        public void Log(LogMessage message)
        {
            Trace.TraceInformation(JsonConvert.SerializeObject(message, GetSettings()));
        }
    }

    public class LogMessage
    {
        public string Environment { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Category { get; set; }
        public string AppContext { get; set; }
        
        public  LogMessage()
        {
            Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            Category = "Info";
            AppContext = "Logger";
        }
    }
}