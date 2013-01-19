using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Models.Admin;
using CrossfitBenchmarks.WebUi.Utility;

namespace CrossfitBenchmarks.WebUi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUIDataService service;
        
        public async Task<ActionResult> Index()
        {
            var model = new AdminIndexViewModel();
            model.WorkoutTypes = await service.GetServiceDataAsync<IEnumerable<WorkoutTypeDto>>("WorkoutTypes");
            return View(model);
        }


        public AdminController(IUIDataService service)
        {
            this.service = service;
        }
    }
}
