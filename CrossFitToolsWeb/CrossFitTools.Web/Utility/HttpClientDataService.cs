using System;
using System.Net.Http;
using System.Threading.Tasks;
using CrossfitBenchmarks.WebUi.Exceptions;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public class HttpClientDataService : IUIDataService
    {
        
        

        
        public HttpClientDataService()
        {
            
            
            
            
            
        }

        public async Task<T> GetServiceDataAsync<T>(string endpoint, object id = null)
        {
            using (HttpClient client = new HttpClient())
            {
                
                HttpResponseMessage response = null;
                if (id != null)
                    response = await client.GetAsync(HttpClientUtilities.GetServiceUri(endpoint) + "/" + id.ToString());
                else
                    response = await client.GetAsync(HttpClientUtilities.GetServiceUri(endpoint));
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

        public async Task<T> PostServiceDataAndAwaitResponseAsync<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                
                var response = await client.PostAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
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

        public async Task<T> PutDataWithResult<T>(string endpoint, T objectData)
        {
            using (HttpClient client = new HttpClient())
            {
                
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
                
                var response = await client.PostAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode(); 
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
                
                var response = await client.PutAsJsonAsync(HttpClientUtilities.GetServiceUri(endpoint), objectData);
                try
                {
                    response.EnsureSuccessStatusCode(); 
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
                    response.EnsureSuccessStatusCode(); 
                }
                catch (HttpRequestException)
                {
                    throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}

