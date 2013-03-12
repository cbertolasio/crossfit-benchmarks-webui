using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using System.Security.Claims;
using CrossfitBenchmarks.WebUi.Utility;

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

            var uri = String.Format("{0}{1}", Constants.RootDomain, "facebookobjects/personalrecord");
            parameters["personal_record"] = uri;
            parameters["og:type"] = "everywod:personal_record";
            parameters["og:url"] = uri;
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
