using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public static class HttpClientUtilities
    {
        /// <summary>
        /// Gets the base URI for the Business Services
        /// </summary>
        /// <returns>The base URI for the Business Services</returns>
        public static string GetRootUri()
        {
            // Get the root URI from Web.config
            var uri = ConfigurationManager.AppSettings["BusinessServicesUrlBase"];
            return uri;
        }

        /// <summary>
        /// Gets the Web API Service URL for a specific service
        /// </summary>
        /// <param name="service">Name of the Service (i.e. productionclass)</param>
        /// <returns>The full URI string for the specified service</returns>
        public static string GetServiceUri(string service)
        {
            return GetRootUri() + "api/" + service;
        }
    }
}
