using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CrossFitTools.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTimeManager dateTimeManager;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.DateTime = dateTimeManager.GetDateTime().ToShortDateString();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public HomeController(IDateTimeManager dateTimeManager)
        {
            this.dateTimeManager = dateTimeManager;
        }
    }

    public interface IDateTimeManager
    {
        DateTime GetDateTime();
    }

    public class DateTimeManager : IDateTimeManager
    {
        
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
        public  DateTimeManager()
        {
            
        }
    }


}
