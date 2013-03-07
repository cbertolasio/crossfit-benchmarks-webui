using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using System.Security.Claims;

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
            var claimsIdentity = (ClaimsIdentity)identity;
            if (claimsIdentity.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value == "Facebook-460497347351482")
            {
                var context = new OpenGraphActionContext { IsAPersonalRecord = isAPersonalRecord, LogEntryType = logEntryType, Identity = identity, LogEntryData = dto };
                var action = actionFactory.Get(context);
                action.Publish();
            }
        } 
            
    }
}
