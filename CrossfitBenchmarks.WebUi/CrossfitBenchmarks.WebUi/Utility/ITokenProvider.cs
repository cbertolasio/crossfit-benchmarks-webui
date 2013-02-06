using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using CrossfitBenchmarks.WebUi.Controllers;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public interface ITokenProvider
    {
        string GetTokenFromACS(string scope);
        string GetOAuthHeader(string token);
    }

    public class TokenProvider : ITokenProvider
    {
        
        public TokenProvider()
        {
            
        }

        #region ITokenProvider Members

        public string GetTokenFromACS(string scope)
        {
            string oauthUserName = ConfigurationManager.AppSettings.Get("OAuthUserName");
            //string oauthUserName = "android-client";
            string oauthPassword = ConfigurationManager.AppSettings.Get("OAuthPassword");
            //string oauthPassword = "p@ssw0rd";

            WebClient client = new WebClient();

            try
            {
                client.BaseAddress = string.Format(
                CultureInfo.InvariantCulture, "https://{0}.{1}",
                SamplesConfiguration.ServiceNamespace,
                SamplesConfiguration.AcsHostUrl);

                NameValueCollection values = new NameValueCollection();
                values.Add("grant_type", "client_credentials");
                values.Add("client_id", oauthUserName);
                values.Add("client_secret", oauthPassword);
                values.Add("scope", scope);

                byte[] responseBytes = client.UploadValues("/v2/OAuth2-13", "POST", values);

                //
                // Extract the access token and return it.
                //
                using (MemoryStream responseStream = new MemoryStream(responseBytes)) {
                    CrossfitBenchmarks.WebUi.Utility.OAuth2TokenResponse tokenResponse =
                        (CrossfitBenchmarks.WebUi.Utility.OAuth2TokenResponse)new DataContractJsonSerializer(typeof(CrossfitBenchmarks.WebUi.Utility.OAuth2TokenResponse)).ReadObject(responseStream);

                    return tokenResponse.access_token;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
            
        }

        public string GetOAuthHeader(string token)
        {
            return string.Format("Bearer {0}", token);
        }

        #endregion
    }
}

