using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossfitBenchmarks.WebUi.Models.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<CrossfitBenchmarks.Data.DataTransfer.WorkoutTypeDto> WorkoutTypes  { get; set; }
    }
}