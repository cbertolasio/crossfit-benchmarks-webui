using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CrossfitBenchmarks.WebUi.Extensions
{
    public static class DateTimeHelper
    {
        public static DateTimeOffset Combine(DateTimeOffset? originalDate, string timeString, int timeZoneOffsetInMinutes)
        {
            if(originalDate.HasValue)
            { 
                return FromString(GetDateAsString(originalDate.Value.Date, timeString, timeZoneOffsetInMinutes));
            }
            else
            {
                throw new InvalidOperationException("original must have a value");
            }
        }

        public static DateTime Combine(DateTime originalDate, string timeString, int timeZoneOffsetInMinutes)
        {
            return FromString(GetDateAsString(originalDate, timeString, timeZoneOffsetInMinutes)).DateTime;;
        }

        private static DateTimeOffset FromString(string dateToParse)  
        {
            return DateTimeOffset.ParseExact(dateToParse, "MM/dd/yyyy h:mm tt zzz", CultureInfo.InvariantCulture);
        }
        
        private static string GetDateAsString(DateTime dateComponent, string timeComponent, int offsetInMinutes)
        {
            var timeSpan = TimeSpan.FromMinutes(offsetInMinutes * -1);
            var offsetString = string.Format("{0:+00;-00}{1:00}", timeSpan.Hours, timeSpan.Minutes);
            return string.Format("{0} {1} {2}", dateComponent.ToString("MM/dd/yyyy"), timeComponent, offsetString);
        }
    }
}