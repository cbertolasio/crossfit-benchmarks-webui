using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class BasicWodOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {
        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();

            parameters["basic_workout"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/basicworkout";
            parameters["og:type"] = "everywod:basic_workout";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/basicworkout";
            parameters["og:title"] = "Basic WOD";
            parameters["score"] = "220";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }

            return result.ToString();
        }
        public BasicWodOpenGraphAction(OpenGraphActionContext context)
            : base(context)
        {

        }
    }
}
