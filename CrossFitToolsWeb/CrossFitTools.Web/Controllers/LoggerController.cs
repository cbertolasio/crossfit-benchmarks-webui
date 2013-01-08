using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrossFitTools.Web.Models.Logger;

namespace CrossFitTools.Web.Controllers
{
    public class LoggerController : Controller
    {
        //
        // GET: /Logger/

        public ActionResult Index(string val)
        {            
            switch (val)
            {
                case "benchmark":
                    return View("Benchmarks", new BenchmarksViewModel(true));

                default:
                    return new EmptyResult();
            }
            
        }

    }
}
