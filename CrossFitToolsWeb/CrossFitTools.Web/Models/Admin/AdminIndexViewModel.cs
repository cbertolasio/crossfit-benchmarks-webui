using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<CrossfitBenchmarks.Data.DataTransfer.WorkoutTypeDto> WorkoutTypes  { get; set; }
    }
}