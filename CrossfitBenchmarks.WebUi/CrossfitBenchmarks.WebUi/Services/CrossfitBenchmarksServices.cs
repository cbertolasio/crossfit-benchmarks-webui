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
        WorkoutLogEntryDto CreateLogEntry(LogEntryDto dto);
        IEnumerable<WorkoutLogEntryDto> GetTheGirls();
        IEnumerable<WorkoutLogEntryDto> GetTheHeros();
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

        public IEnumerable<WorkoutLogEntryDto> GetTheHeros()
        {
            var baseUri = "TheHeros";
            return GetWorkoutLogEntries(baseUri).Data;
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