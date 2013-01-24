using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Utility;

namespace CrossfitBenchmarks.WebUi.Services
{
    public interface ICrossfitBenchmarksServices
    {
        IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId);
    }

    public class CrossfitBenchmarksServices : ICrossfitBenchmarksServices
    {
        public IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId)
        {
            var task = Task.Factory.StartNew(async() => {
                var data = (await dataProvider.GetServiceDataAsync<IEnumerable<WorkoutLogEntryDto>>("TheBenchmarks", userId));
                return data;
            });



            return task.Result.Result;
        }

        public CrossfitBenchmarksServices(IUIDataService dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        private readonly IUIDataService dataProvider;
    }
}