using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public interface IOpenGraphServices
    {
        void PublishAction(LogEntryDto dto, IIdentity identity, string logEntryType, bool isAPersonalRecord);
        
    }
}
