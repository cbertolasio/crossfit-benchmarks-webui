using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{
    
    public class TheHeroesViewModel
    {
        public TheHeroesViewModel()
        {
            WodList = new List<WodItemViewModel>();
        }

        public List<WodItemViewModel> WodList { get; set; }
    }
}
