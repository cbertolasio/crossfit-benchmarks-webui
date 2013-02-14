using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{

    public class BenchmarksViewModel
    {
        public List<WodItemViewModel> WodList { get; set; }

        public BenchmarksViewModel(bool createSampleData)
        {
            WodList = new List<WodItemViewModel>();

            var item = new WodItemViewModel {
                Id = 1,
                LastAttemptDate = new DateTime(2012, 12, 18, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2012, 10, 13, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "6:30",
                Name = "Run 1600m",
                PersonalRecordScore = "5:45"
            };

            WodList.Add(item);

            item = new WodItemViewModel {
                Id = 2,
                LastAttemptDate = new DateTime(2013, 01, 01, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2013, 01, 01, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "21",
                Name = "Max Pullups",
                PersonalRecordScore = "21"
            };

            WodList.Add(item);

            item = new WodItemViewModel {
                Id = 3,
                LastAttemptDate = new DateTime(2013, 01, 06, 12, 00, 00, 00, DateTimeKind.Local),
                LastPersonalRecordDate = new DateTime(2013, 01, 06, 12, 00, 00, 00, DateTimeKind.Local),
                LastScore = "345 #",
                Name = "High Bar Back Squat",
                PersonalRecordScore = "345 #"
            };

            WodList.Add(item);

            item = new WodItemViewModel {
                Id = 4,
                LastAttemptDate = DateTime.MinValue,
                LastPersonalRecordDate = DateTime.MinValue,
                LastScore = null,
                Name = "Clean & Jerk",
                PersonalRecordScore = string.Empty
            };

            WodList.Add(item);
        }

        public  BenchmarksViewModel()
        {
            WodList = new List<WodItemViewModel>();
        }
    }
}

