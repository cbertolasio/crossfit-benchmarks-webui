using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using CrossfitBenchmarks.WebUi.Utility;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class BasicWodOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {
        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();

            var uri = String.Format("{0}{1}", Constants.RootDomain, "facebookobjects/basicworkout");
            parameters["basic_workout"] = uri;
            parameters["og:type"] = "everywod:basic_workout";
            parameters["og:url"] = uri;
            parameters["og:title"] = "Basic WOD";
            parameters["score"] = "220";
            parameters["workout_name"] = "SampleWorkoutName";

            //jbloggs 100005300791368 jbloggs_oooawbp_jbloggs@tfbnw.net 
            var result = client.Post("me/everywod:log", parameters);
            if (result == null)
            {
                result = "null value was returned...";
            }
            else
            {
                LogSuccess("BasicWOD", "A user posted a basic wod");
            }

            
            return result.ToString();
        }
        public BasicWodOpenGraphAction(OpenGraphActionContext context)
            : base(context)
        {

        }
    }
}
