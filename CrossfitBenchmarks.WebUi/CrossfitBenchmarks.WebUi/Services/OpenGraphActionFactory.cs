using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public class OpenGraphActionFactory : IOpenGraphActionFactory
    {
        public IOpenGraphAction Get(string logEntryType, bool isAPersonalRecord)
        {
            if (isAPersonalRecord)
                return new PersonalRecordOpenGraphAction();

            switch (logEntryType)
            {
                case "G":
                    return new TheGirlsOpenGraphAction();
                case "H":
                    return new TheHerosOpenGraphAction();
                case "B":
                    return new BenchmarkOpenGraphAction();

                default:
                    throw new InvalidOperationException(string.Format("unable to create an Open Graph Action for logEntryType: '{0}'", logEntryType));
            }

            //throw new InvalidOperationException("code should not execute...");
        }
    }
}
