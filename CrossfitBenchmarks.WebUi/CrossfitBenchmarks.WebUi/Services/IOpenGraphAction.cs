using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;

namespace CrossfitBenchmarks.WebUi.Services
{
    public interface IOpenGraphAction
    {
        string Publish();
    }
}
