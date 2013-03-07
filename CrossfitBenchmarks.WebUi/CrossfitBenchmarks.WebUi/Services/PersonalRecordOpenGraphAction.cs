using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using System.Security.Claims;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class PersonalRecordOpenGraphAction : OpenGraphActionBase, IOpenGraphAction
    {

        public PersonalRecordOpenGraphAction(OpenGraphActionContext context):base(context)
        {

        }

        public string Publish()
        {
            var accessToken = GetAccessToken();
            var client = GetClient();
            var parameters = GetBaseParameters();
            
            parameters["personal_record"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/personalrecord";
            parameters["og:type"] = "everywod:personal_record";
            parameters["og:url"] = "http://crossfitbenchmarks.azurewebsites.net/facebookobjects/personalrecord";
            parameters["og:title"] = "Personal Record";
            parameters["score"] = "2:30";

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
