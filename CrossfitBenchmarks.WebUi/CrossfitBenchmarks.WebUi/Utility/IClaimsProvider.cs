using System;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public interface IClaimsProvider
    {
        string GetNameIdentifier();
        string GetIdentityProvider();
    }
}

