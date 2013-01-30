using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;
using RestSharp.Serializers;

namespace CrossfitBenchmarks.WebUi.Services
{
    public interface ICrossfitBenchmarksServices
    {
        LogEntryDto CreateLogEntry(LogEntryDto dto);
        IEnumerable<WorkoutLogEntryDto> GetTheGirls(string userId);
        IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId);
    }

    public class CrossfitBenchmarksServices : ICrossfitBenchmarksServices
    {
        public LogEntryDto CreateLogEntry(LogEntryDto dto)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("LogEntry", RestSharp.Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(dto);
            request.JsonSerializer = new JsonSerializer();

            var response = client.Execute<LogEntryDto>(request);
            return response.Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("TheBenchmarks/{id}", RestSharp.Method.GET);
            request.JsonSerializer = new JsonSerializer();
            request.AddUrlSegment("id", userId);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute<List<WorkoutLogEntryDto>>(request);
            return response.Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheGirls(string userId)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("TheBenchmarks/{id}", RestSharp.Method.GET);
            request.JsonSerializer = new JsonSerializer();
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