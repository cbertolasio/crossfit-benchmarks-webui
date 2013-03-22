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
        bool DeleteAllLogEntries(int workoutId);

        bool DeleteLogEntry(int workoutLogEntryId);
        WorkoutLogEntryDto CreateLogEntry(LogEntryDto dto);
        string GetSummary();
        IEnumerable<WorkoutLogEntryDto> GetTheGirls();
        IEnumerable<WorkoutLogEntryDto> GetTheHeroes();
        string GetWorkoutHistory(int id);
        IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks();
    }

    public class CrossfitBenchmarksServices : ICrossfitBenchmarksServices
    {
        private void AddHeaders(RestRequest request)
        {
            request.AddAuthorizationHeader(tokenProvider, scope);
            //request.AddIdentityProviderHeader(claimsProvider.GetIdentityProvider());
            //request.AddNameIdentifierHeader(claimsProvider.GetNameIdentifier());

            request.AddParameter("nameIdentifier", claimsProvider.GetNameIdentifier(), ParameterType.GetOrPost);
            request.AddParameter("identityProvider", claimsProvider.GetIdentityProvider(), ParameterType.GetOrPost);

            //request.AddParameter("nameIdentifier", claimsProvider.GetNameIdentifier(), ParameterType.RequestBody);
            //request.AddParameter("identityProvider", claimsProvider.GetIdentityProvider(), ParameterType.RequestBody);
        }

        public WorkoutLogEntryDto CreateLogEntry(LogEntryDto dto)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("LogEntry", RestSharp.Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddAuthorizationHeader(tokenProvider, scope);
            dto.UserInfo = new UserInfoDto { IdentityProvider = claimsProvider.GetIdentityProvider(), NameIdentifier = claimsProvider.GetNameIdentifier() };
            request.AddBody(dto);

            request.JsonSerializer = new JsonSerializer();

            var response = client.Execute<WorkoutLogEntryDto>(request);
            return response.Data;
        }

        private static bool GetBooleanResponse(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return Boolean.Parse(response.Content);
        }

        private void AddParametersForDelete(int workoutLogEntryId, RestRequest request)
        {
            request.AddParameter("id", workoutLogEntryId);
            request.AddParameter("ip", claimsProvider.GetIdentityProvider());
            request.AddParameter("nid", claimsProvider.GetNameIdentifier());
        }

        public bool DeleteLogEntry(int workoutLogEntryId)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("LogEntry", RestSharp.Method.DELETE);
            request.AddAuthorizationHeader(tokenProvider, scope);
            AddParametersForDelete(workoutLogEntryId, request);

            return GetBooleanResponse(client, request);
        }

        public bool DeleteAllLogEntries(int workoutId)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest("WorkoutLogEntries", RestSharp.Method.DELETE);
            request.AddAuthorizationHeader(tokenProvider, scope);
            AddParametersForDelete(workoutId, request);

            return GetBooleanResponse(client, request);
        }

        public string GetSummary()
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseODataUri().ToString());
            var request = new RestSharp.RestRequest("WorkoutLogs", RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddAuthorizationHeader(tokenProvider, scope);
            request.AddParameter("$filter", string.Format("UserNameIdentifier eq '{0}'", claimsProvider.GetNameIdentifier()));
            request.AddParameter("$top", "5");
            request.AddParameter("$orderby", "DateOfWod desc,WorkoutLogId desc");
            request.AddParameter("$inlinecount", "allpages");
            request.JsonSerializer = new JsonSerializer();
            return client.Execute(request).Content;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheBenchmarks()
        {
            var baseUri = "TheBenchmarks";
            return GetWorkoutLogEntries(baseUri).Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheGirls()
        {
            string baseUri = "TheGirls";
            return GetWorkoutLogEntries(baseUri).Data;
        }

        public IEnumerable<WorkoutLogEntryDto> GetTheHeroes()
        {
            var baseUri = "TheHeroes";
            return GetWorkoutLogEntries(baseUri).Data;
        }

        public string GetWorkoutHistory(int id)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseODataUri().ToString());
            var request = new RestSharp.RestRequest("WorkoutLogs", RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddAuthorizationHeader(tokenProvider, scope);
            request.AddParameter("$filter", string.Format("UserNameIdentifier eq '{0}' and WorkoutId eq {1}", claimsProvider.GetNameIdentifier(), id));
            request.AddParameter("$top", "25");
            request.AddParameter("$orderby", "DateOfWod desc,WorkoutLogId desc");
            request.AddParameter("$inlinecount", "allpages");
            request.JsonSerializer = new JsonSerializer();
            return client.Execute(request).Content;
        }

        private IRestResponse<List<WorkoutLogEntryDto>> GetWorkoutLogEntries(string baseUri)
        {
            var client = new RestSharp.RestClient(HttpClientUtilities.GetBaseUri().ToString());
            var request = new RestSharp.RestRequest(baseUri, RestSharp.Method.GET);
            request.JsonSerializer = new JsonSerializer();
            AddHeaders(request);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<List<WorkoutLogEntryDto>>(request);
            return response;
        }

        public CrossfitBenchmarksServices(ITokenProvider tokenProvider, IClaimsProvider claimsProvider)
        {
            this.claimsProvider = claimsProvider;
            this.tokenProvider = tokenProvider;
            this.scope = ConfigurationManager.AppSettings["AcsScope"];
        }

        private readonly IClaimsProvider claimsProvider;
        private string scope;
        private readonly ITokenProvider tokenProvider;
    }
}