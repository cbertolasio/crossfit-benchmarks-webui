using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using CrossfitBenchmarks.WebUi.Utility;

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

            var uri = String.Format("{0}{1}", Constants.RootDomain, "facebookobjects/benchmark");
            parameters["benchmark"] = uri;
            parameters["og:type"] = "everywod:benchmark";
            parameters["og:url"] = uri;
            parameters["og:title"] = "Benchmark";
            parameters["score"] = "220#";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            else {
                LogSuccess("Benchamrk", "A user posted a benchmark");
            }
            return result.ToString();
        }
    }
}
