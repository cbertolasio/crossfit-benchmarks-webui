using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;

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
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("TheBenchmarks/{id}", RestSharp.Method.GET);
            request.AddUrlSegment("id", userId);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute<List<WorkoutLogEntryDto>>(request);
            return response.Data;
        }

        public CrossfitBenchmarksServices()
        {
        }

        private readonly IUIDataService dataProvider;
    }
}