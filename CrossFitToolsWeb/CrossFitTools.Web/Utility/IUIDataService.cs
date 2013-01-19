using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public interface IUIDataService
    {
        Task<T> GetServiceDataAsync<T>(string endpoint, object id = null);
        Task PostServiceDataAsync<T>(string endpoint, T objectData);
        Task<T> PostServiceDataAndAwaitResponseAsync<T>(string endpoint, T objectData);
        Task PutServiceDataAsync<T>(string endpoint, T objectData);

        /// <summary>
        /// implementors should make sure that HttpPut is required, that T comes from the body, and T is returned
        /// </summary>
        Task<T> PutDataWithResult<T>(string endpoint, T objectData);
        Task DeleteServiceDataAsync(string endpoint, object id = null);
    }
}

