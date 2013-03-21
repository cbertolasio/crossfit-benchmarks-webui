using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitBenchmarks.WebUi.Services;
using CrossfitBenchmarks.WebUi.Utility;
using CrossFitTools.Web.Controllers;
using Ninject.Modules;

namespace CrossFitTools.Web.Modules
{
    public class NinjectModules : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDateTimeManager>().To<DateTimeManager>();
            //Bind<IUIDataService>().To<HttpClientDataService>();
            Bind<ICrossfitBenchmarksServices>().To<CrossfitBenchmarksServices>();
            Bind<ITokenProvider>().To<TokenProvider>().InSingletonScope();
            Bind<IClaimsProvider>().To<ClaimsProvider>();

            Bind<IOpenGraphActionFactory>().To<OpenGraphActionFactory>();
            Bind<IOpenGraphServices>().To<OpenGraphService>();

            Bind<ILogger>().To<UnifiedLogger>();
        }
    }
}