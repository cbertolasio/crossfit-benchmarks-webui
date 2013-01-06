using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrossFitTools.Web.Models.Calculator;

namespace CrossFitTools.Web.Controllers
{
    public class CalculatorController : Controller
    {
        //
        // GET: /Calculator/

        public ActionResult Index()
        {
            var model = new CalculatorViewModel();
            return View(model);
        }

    }
}
