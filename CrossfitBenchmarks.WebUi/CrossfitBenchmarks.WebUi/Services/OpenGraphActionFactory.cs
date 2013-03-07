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
        public IOpenGraphAction Get(OpenGraphActionContext context)
        {
            
            if (context.IsAPersonalRecord)
                return new PersonalRecordOpenGraphAction(context);

            switch (context.LogEntryType)
            {
                case "G":
                    return new TheGirlsOpenGraphAction(context);
                case "H":
                    return new TheHerosOpenGraphAction(context);
                case "B":
                    return new BenchmarkOpenGraphAction(context);
                case "BasicWod":
                    return new BasicWodOpenGraphAction(context);


                default:
                    throw new InvalidOperationException(string.Format("unable to create an Open Graph Action for logEntryType: '{0}'", context.LogEntryType));
            }

        }
    }
}
