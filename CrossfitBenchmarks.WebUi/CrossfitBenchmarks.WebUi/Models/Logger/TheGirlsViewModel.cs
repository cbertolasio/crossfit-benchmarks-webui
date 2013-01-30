using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{
    
    public class TheGirlsViewModel
    {
        public List<WodItemViewModel> WodList { get; set; }

        public TheGirlsViewModel()
        {
            WodList = new List<WodItemViewModel>();
        }
    }
}
