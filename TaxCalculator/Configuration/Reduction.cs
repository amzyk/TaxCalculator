using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Configuration
{
    public class Reduction
    {
        public ReductionStage FirstStage { get; set; }
        public ReductionStage SecondStage { get; set; }
        public ReductionStage ThirdStage { get; set; }
        public ReductionStage FourthStage { get; set; }
    }
}
