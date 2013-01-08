using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossFitTools.Web.Models.Logger
{
    public class BenchmarksViewModel
    {
        public List<BenchmarkItemViewModel> Benchmarks { get; set; }

        public BenchmarksViewModel(bool createSampleData)
        {
            Benchmarks = new List<BenchmarkItemViewModel>();

            var item = new BenchmarkItemViewModel {
                Id = 1,
                LastAttemptDate = new DateTime(2012, 12, 18, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2012, 10, 13, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "6:30",
                Name = "Run 1600m",
                PersonalRecordScore = "5:45"
            };

            Benchmarks.Add(item);

            item = new BenchmarkItemViewModel {
                Id = 2,
                LastAttemptDate = new DateTime(2013, 01, 01, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2013, 01, 01, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "21",
                Name = "Max Pullups",
                PersonalRecordScore = "21"
            };

            Benchmarks.Add(item);

            item = new BenchmarkItemViewModel {
                Id = 3,
                LastAttemptDate = new DateTime(2013, 01, 06, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2013, 01, 06, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "345 #",
                Name = "High Bar Back Squat",
                PersonalRecordScore = "345 #"
            };

            Benchmarks.Add(item);

            item = new BenchmarkItemViewModel {
                Id = 4,
                LastAttemptDate = DateTime.MinValue,
                LastPersonalRecordDate = DateTime.MinValue,
                LastScore = null,
                Name = "Clean & Jerk",
                PersonalRecordScore = string.Empty
            };

            Benchmarks.Add(item);
        }

        public  BenchmarksViewModel()
        {
            Benchmarks = new List<BenchmarkItemViewModel>();
        }
    }
}

