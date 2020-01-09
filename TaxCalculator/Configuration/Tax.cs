using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Configuration
{
    public class Tax
    {
        public TaxStage FirstStage { get; set; }
        public TaxStage SecondStage { get; set; }
        public TaxStage ThirdStage { get; set; }
    }
}
