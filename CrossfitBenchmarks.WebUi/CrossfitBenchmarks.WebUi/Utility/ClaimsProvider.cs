using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CrossfitBenchmarks.WebUi.Controllers;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public class ClaimsProvider : IClaimsProvider
    {
        private ClaimsIdentity claimsIdentity;

        public  ClaimsProvider()
        {
            claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
        }

        public string GetNameIdentifier()
        {
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            return null;
        }

        public string GetIdentityProvider()
        {
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                return claimsIdentity.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value;
            }
            return null;
        }
    }
}

