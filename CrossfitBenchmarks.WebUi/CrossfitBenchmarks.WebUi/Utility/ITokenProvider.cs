using System;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public interface ITokenProvider
    {
        string GetTokenFromACS(string scope);
        string GetOAuthHeader(string token);
    }
}