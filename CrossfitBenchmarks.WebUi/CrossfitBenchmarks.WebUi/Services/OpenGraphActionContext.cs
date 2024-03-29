using CrossfitBenchmarks.Data.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using CrossfitBenchmarks.WebUi.Utility;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class OpenGraphActionContext
    {
        public IIdentity Identity { get; set; }
        public LogEntryDto LogEntryData { get; set; }
        public string LogEntryType { get; set; }
        public bool IsAPersonalRecord { get; set; }

        public ILogger Logger { get; set; }

        public OpenGraphActionContext()
        {
        }
    }
}
