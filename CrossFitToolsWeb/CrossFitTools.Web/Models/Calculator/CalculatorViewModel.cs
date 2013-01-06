using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossFitTools.Web.Models.Calculator
{
    public class CalculatorViewModel
    {
        public List<PercentageItem> Percentages { get; set; }
        public PercentageItem ItemTemplate { get; set; }
        public int Weight { get; set; }

        public CalculatorViewModel()
        {
            Percentages = new List<PercentageItem>();
            ItemTemplate = new PercentageItem();
            Weight = 95;
        }
    }


    public class PercentageItem
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public  PercentageItem()
        {
            
        }
    }


}