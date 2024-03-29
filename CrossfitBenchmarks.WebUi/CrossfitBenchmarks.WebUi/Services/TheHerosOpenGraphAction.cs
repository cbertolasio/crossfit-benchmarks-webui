using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using CrossfitBenchmarks.WebUi.Utility;

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

            parameters["hero_workout"] = String.Format("{0}{1}", Constants.RootDomain,"facebookobjects/heroworkout");
            parameters["og:type"] = "everywod:hero_workout";
            parameters["og:url"] = String.Format("{0}{1}", Constants.RootDomain, "facebookobjects/heroworkout");
            parameters["og:title"] = "Hero WOD";
            parameters["score"] = "220";
            parameters["workout_name"] = "SampleWorkoutName";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            else {
                LogSuccess("TheHeroes", "A user posted one of the heroes");
            }

            return result.ToString();
        }
    }
}
