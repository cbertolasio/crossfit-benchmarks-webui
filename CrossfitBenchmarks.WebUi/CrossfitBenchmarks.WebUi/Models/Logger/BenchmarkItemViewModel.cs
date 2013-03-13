using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{
    public class WodItemViewModel
    {
        public int Id { get; set; }
        public string LastAttemptDateAsString { get; set; }
        public string LastPersonalRecordDateAsString { get; set; }
        public string Name { get; set; }
        public string LastScore { get; set; }
        public string PersonalRecordScore { get; set; }
        public DateTimeOffset LastAttemptDate { get; set; }
        public DateTimeOffset LastPersonalRecordDate { get; set; }
    }
}