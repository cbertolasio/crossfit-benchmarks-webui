using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using CrossfitBenchmarks.WebUi.Exceptions;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public class HttpClientDataService : IUIDataService
    {
        //private ICurrentUserService _userService;
        //private bool _useEncryption = true;

        //public HttpClientDataService(ICurrentUserService userService)
        public HttpClientDataService()
        {
            //if (userService == null)
            //    throw new ArgumentNullException("userService");
            //_userService = userService;
            //if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["TokenEncryptionEnabled"]) && ConfigurationManager.AppSettings["TokenEncryptionEnabled"].ToLower() == "false")
            //    _useEncryption = false;
        }

        public async Task<T> GetServiceDataAsync<T>(string endpoint, object id = null)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);
                HttpResponseMessage response = null;
                if (id != null)
                    response = await client.GetAsync(HttpClientUtilities.GetServiceUri(endpoint) + "/" + id.ToString());
                else
                    response = await client.GetAsync(HttpClientUtilities.GetServiceUri(endpoint));
                try
                {
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostServiceDataAndAwaitResponseAsync<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);
                var response = await client.PostAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PutDataWithResult<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);
                var response = await client.PutAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }


        public async Task PostServiceDataAsync<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);
                var response = await client.PostAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        public async Task PutServiceDataAsync<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);
                var response = await client.PutAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }
            }
        }




        public async Task DeleteServiceDataAsync(string endpoint, object id = null)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.SetAuthHeaders(_userService, _useEncryption);

                var uri = string.Empty;
                if (id != null)
                {
                    uri = string.Format("{0}?id={1}", HttpClientUtilities.GetServiceUri(endpoint), id.ToString());
                }
                else
                {
                    uri = HttpClientUtilities.GetServiceUri(endpoint);
                }


                var response = await client.DeleteAsync(uri);

                try
                {
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}

