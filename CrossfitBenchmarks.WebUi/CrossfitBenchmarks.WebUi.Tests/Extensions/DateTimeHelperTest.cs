using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CrossfitBenchmarks.WebUi.Extensions;
using NUnit.Framework;

namespace CrossfitBenchmarks.WebUi.Tests.Extensions
{
/// <summary>
/// the basic premise is that...
/// the user will pick a date using a date picker
/// the user will pick a time using a time picker
/// the moment js library will calculate the users timezone offset in minutes - based on their current browser setting
/// we need to take all this data and return a real DateTimeOffset or DateTime struct
/// this needs to work for standard, non-standard, and UTC time zones
/// </summary>
[TestFixture]
public class DateTimeHelperTest
{
    [TestCase("10:30 PM", "01/31/2013", 360, Description = "test MST time zone (UTC -6)")]
    [TestCase("4:30 PM", "01/31/2013", -120, Description = "tests E. Europe time zone (UTC +2)")]
    [TestCase("4:30 AM", "01/31/2013", -330, Description = "tests Mumbai time zone (UTC +5:30)")]
    [TestCase("12:45 AM", "01/31/2013", -330, Description = "tests Mumbai time zone (UTC +5:30)")]
    [TestCase("04:30 PM", "01/31/2013", 0, Description = "tests Casablanca time zone (UTC)")] 
    public void Combine_Returns_Expected_DateTimeOffset(string timeString, string datePart, int offsetInMinutes)
    {
        var testDate = DateTimeOffset.Parse(datePart);

        var actual = DateTimeHelper.Combine(testDate, timeString, offsetInMinutes);

        Assert.AreEqual(GetExpected(timeString, datePart, offsetInMinutes), actual);
        Console.WriteLine("expected: {0}, actual: {1}", GetExpected(timeString, datePart, offsetInMinutes), actual);
    }

        [TestCase("10:30 PM", "01/31/2013", 360, Description = "test MST time zone (UTC -6)")]
        [TestCase("4:30 PM", "01/31/2013", -120, Description = "tests E. Europe time zone (UTC +2)")]
        [TestCase("4:30 AM", "01/31/2013", -330, Description = "tests Mumbai time zone (UTC +5:30)")]
        [TestCase("04:30 PM", "01/31/2013", 0, Description = "tests Casablanca time zone (UTC)")]
        public void CombineReturnsExpectedDate(string timeString, string datePart, int offsetInMinutes)
        {
            var testDate = DateTime.Parse(datePart);
            var actual = DateTimeHelper.Combine(testDate, timeString, offsetInMinutes);
            Assert.AreEqual(GetExpected(timeString, datePart, offsetInMinutes).DateTime, actual);
            Console.WriteLine("expected: {0}, actual: {1}", GetExpected(timeString, datePart, offsetInMinutes).DateTime , actual);
        }

    /// <summary>
    /// test method that mimics the code/algorithm under test
    /// </summary>
    private static DateTimeOffset GetExpected(string timeString, string datePart, int offsetInMinutes)
    {
        var timeSpan = TimeSpan.FromMinutes(offsetInMinutes * -1);
        var offsetString = string.Format("{0:+00;-00}{1:00}", timeSpan.Hours, timeSpan.Minutes);
        var expected = DateTimeOffset.ParseExact(string.Format("{0} {1} {2}", datePart, timeString, offsetString), "MM/dd/yyyy h:mm tt zzz", CultureInfo.InvariantCulture);
        return expected;
    }
}
}
