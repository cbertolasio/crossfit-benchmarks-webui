using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrossFitTools.Web.Models.Calculator;

namespace CrossFitTools.Web.Controllers
{
     [Authorize]
    public class CalculatorController : Controller
    {
        
        

        public ActionResult Index()
        {
            var model = new CalculatorViewModel();
            return View(model);
        }

    }
}
