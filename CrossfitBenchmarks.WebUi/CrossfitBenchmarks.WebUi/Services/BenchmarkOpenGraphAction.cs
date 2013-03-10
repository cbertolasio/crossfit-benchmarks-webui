using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class BenchmarkOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {

        public BenchmarkOpenGraphAction(OpenGraphActionContext context) : base(context)
        {

        }

        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();

            parameters["benchmark"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/benchmark";
            parameters["og:type"] = "everywod:benchmark";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/benchmark";
            parameters["og:title"] = "Benchmark";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            parameters["score"] = "220#";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }

            return result.ToString();
        }
    }
}
