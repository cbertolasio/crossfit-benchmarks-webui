using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CrossfitBenchmarks.WebUi.Utility
{

    [DataContract]
    class OAuth2TokenResponse
    {
        [DataMember]
        public string access_token;
    }
}

