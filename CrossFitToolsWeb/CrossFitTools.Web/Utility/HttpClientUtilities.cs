using System;
using System.Configuration;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public static class HttpClientUtilities
    {
        /// <summary>
        /// Gets the base URI for the Business Services
        /// </summary>
        /// <returns>The base URI for the Business Services</returns>
        public static Uri GetRootUri()
        {
            
            var uri = ConfigurationManager.AppSettings["BusinessServicesUrlBase"];
            return new Uri(uri);
        }

        /// <summary>
        /// Gets the Web API Service URL for a specific service
        /// </summary>
        /// <param name="service">Name of the Service (i.e. productionclass)</param>
        /// <returns>The full URI string for the specified service</returns>
        public static Uri GetBaseUri()
        {
            return new Uri(GetRootUri(), "api/");
        }

        public static Uri GetServiceUri(string service)
        {
            return new Uri(GetBaseUri(), service);
        }
    }
}
