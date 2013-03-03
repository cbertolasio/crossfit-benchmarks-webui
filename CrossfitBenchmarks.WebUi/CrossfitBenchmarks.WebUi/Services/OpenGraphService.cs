using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class OpenGraphService : IOpenGraphServices
    {
        private readonly IOpenGraphActionFactory actionFactory;
        public OpenGraphService(IOpenGraphActionFactory actionFactory)
        {
            this.actionFactory = actionFactory;
        }
        public void PublishAction(LogEntryDto dto, IIdentity identity, string logEntryType, bool isAPersonalRecord)
        {
            var action = actionFactory.Get(logEntryType, isAPersonalRecord);
            action.Publish();
        }
    }
}
