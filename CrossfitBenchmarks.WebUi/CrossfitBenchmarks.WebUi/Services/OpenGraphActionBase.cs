using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using System.Security.Claims;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class OpenGraphActionBase
    {

        private readonly OpenGraphActionContext context;
        public OpenGraphActionBase(OpenGraphActionContext context)
        {
            this.context = context;
        }

        protected string GetAccessToken()
        {
            var claimsIdentity = (ClaimsIdentity)context.Identity;
            return claimsIdentity.FindFirst("http://www.facebook.com/claims/AccessToken").Value;
        }

        protected Dictionary<string, object> GetBaseParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters["access_token"] = GetAccessToken();
            parameters["fb:app_id"] = "460497347351482";
            parameters["og:image"] = "http://crossfitbenchmarks.azurewebsites.net/content/images/ew-fbObject-x1500.jpg";
            return parameters;
        }

        protected Facebook.FacebookClient GetClient()
        {
            return new Facebook.FacebookClient(GetAccessToken());
        }
    }
}
