using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class TheHeroesOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {

        public TheHeroesOpenGraphAction(OpenGraphActionContext context):base(context)
        {

        }

        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();

            parameters["hero_workout"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/heroworkout";
            parameters["og:type"] = "everywod:hero_workout";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/heroworkout";
            parameters["og:title"] = "Hero WOD";
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
