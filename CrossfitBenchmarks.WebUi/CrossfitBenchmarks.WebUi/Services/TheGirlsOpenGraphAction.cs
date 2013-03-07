using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class TheGirlsOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {

        public TheGirlsOpenGraphAction(OpenGraphActionContext context)
            : base(context)
        {

        }
        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();

            parameters["girls"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/girlworkout";
            parameters["og:type"] = "everywod:girls";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/girlworkout";
            parameters["og:title"] = "One of the Girls";
            parameters["score"] = "220";

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
