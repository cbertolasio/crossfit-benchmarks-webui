using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{
    
    public class TheHerosViewModel
    {
        public TheHerosViewModel()
        {
            WodList = new List<WodItemViewModel>();
        }

        public List<WodItemViewModel> WodList { get; set; }
    }
}
