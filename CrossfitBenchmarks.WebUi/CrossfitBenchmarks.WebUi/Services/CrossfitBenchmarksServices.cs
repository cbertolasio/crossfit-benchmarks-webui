using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;
using RestSharp.Serializers;
using CrossfitBenchmarks.WebUi.Extensions;
using System.Configuration;

namespace CrossfitBenchmarks.WebUi.Services
{
    public interface ICrossfitBenchmarksServices
    {
        LogEntryDto CreateLogEntry(LogEntryDto dto);
        IEnumerable<WorkoutLogEntryDto> GetTheGirls(string userId);
        IEnumerable<WorkoutLogEntryDto> GetTheHeros(string userId);
        IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId);
    }

    public class CrossfitBenchmarksServices : ICrossfitBenchmarksServices
    {
        public LogEntryDto CreateLogEntry(LogEntryDto dto)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("LogEntry", RestSharp.Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddAuthorizationHeader(tokenProvider, scope);
            request.AddBody(dto);
            request.JsonSerializer = new JsonSerializer();

            var response = client.Execute<LogEntryDto>(request);
            return response.Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks(string userId)
        {
            var baseUri = "TheBenchmarks/{id}";
            return GetWorkoutLogEntries(baseUri, userId).Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheGirls(string userId)
        {
            string baseUri = "TheGirls/{id}";
            return GetWorkoutLogEntries(baseUri, userId).Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheHeros(string userId)
        {
            var baseUri = "TheHeros/{id}";
            return GetWorkoutLogEntries(baseUri, userId).Data;
        }

        private IRestResponse<List<WorkoutLogEntryDto>> GetWorkoutLogEntries(string baseUri, string userId)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest(baseUri, RestSharp.Method.GET);
            request.JsonSerializer = new JsonSerializer();
            request.AddAuthorizationHeader(tokenProvider, scope);
            request.AddUrlSegment("id", userId);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<List<WorkoutLogEntryDto>>(request);
            return response;
        }

        public CrossfitBenchmarksServices(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
            this.scope = ConfigurationManager.AppSettings["AcsScope"];
        }

        private readonly IUIDataService dataProvider;
        private string scope;
        private readonly ITokenProvider tokenProvider;
    }
}