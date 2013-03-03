using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CrossfitBenchmarks.WebUi.Extensions;
using NUnit.Framework;

namespace CrossfitBenchmarks.WebUi.Tests.Extensions
{
    [TestFixture]
    public class DateTimeHelperTest
    {
        [TestCase("10:30 PM", "01/31/2013")]
        [TestCase("4:30 PM", "01/31/2013")]
        [TestCase("4:30 AM", "01/31/2013")]
        [TestCase("04:30 AM", "01/31/2013")]
        public void Combine_Returns_Expected_DateTimeOffset(string timeString, string datePart)
        {
            var testDate = DateTimeOffset.Parse(datePart);
            var actual = DateTimeHelper.Combine(testDate, timeString);
            var expected = DateTimeOffset.ParseExact(string.Format("{0} {1}", datePart, timeString), "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("10:30 PM", "01/31/2013")]
        [TestCase("4:30 PM", "01/31/2013")]
        [TestCase("4:30 AM", "01/31/2013")]
        [TestCase("04:30 PM", "01/31/2013")]
        public void CombineReturnsExpectedDate(string timeString, string datePart)
        {
            var testDate = DateTime.Parse(datePart);
            var actual = DateTimeHelper.Combine(testDate, timeString);
            var expected = DateTime.ParseExact(string.Format("{0} {1}", datePart, timeString), "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, actual);
        }
    }
}
