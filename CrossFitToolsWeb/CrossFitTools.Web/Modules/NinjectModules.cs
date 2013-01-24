using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            Bind<IUIDataService>().To<HttpClientDataService>();
            Bind<ICrossfitBenchmarksServices>().To<CrossfitBenchmarksServices>();
        }
    }
}