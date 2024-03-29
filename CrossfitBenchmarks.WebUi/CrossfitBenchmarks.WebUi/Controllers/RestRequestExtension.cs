using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitBenchmarks.WebUi.Utility;
using RestSharp;

namespace CrossfitBenchmarks.WebUi.Extensions
{
    public static class RestRequestExtension
    {
        public static void AddAuthorizationHeader(this RestRequest request, ITokenProvider tokenProvider, string scope)
        {
            request.AddHeader("Authorization", tokenProvider.GetOAuthHeader(tokenProvider.GetTokenFromACS(scope)));
        }

        public static void AddNameIdentifierHeader(this RestRequest request, string nameIdentifierValue)
        {
            request.AddHeader("NameIdentifier", nameIdentifierValue);
        }
        public static void AddIdentityProviderHeader(this RestRequest request, string identityProviderValue)
        {
            request.AddHeader("IdentityProvider", identityProviderValue);
        }
    }
}

