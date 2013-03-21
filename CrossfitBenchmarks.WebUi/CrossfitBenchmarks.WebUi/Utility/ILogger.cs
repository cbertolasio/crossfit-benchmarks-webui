using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossfitBenchmarks.WebUi.Utility
{
    public interface ILogger
    {
        void Log(LogMessage message);

        void LogError(string message, Exception exception);
        void LogMessage(string message);
    }
}
