using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CrossfitBenchmarks.WebUi.Extensions
{
    public static class DateTimeHelper
    {
        public static DateTimeOffset Combine(DateTimeOffset? original, string timeString)
        {
            if (original.HasValue)
            {
                var timeOfDay = DateTime.ParseExact(timeString, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                return original.Value.Add(timeOfDay);
            }

            throw new InvalidOperationException("original must have a value");
        }

        public static DateTime Combine(DateTime original, string timeString)
        {
            DateTime time = DateTime.ParseExact(timeString, "h:mm tt", CultureInfo.InvariantCulture);
            return original.Add(time.TimeOfDay);
        }
    }
}