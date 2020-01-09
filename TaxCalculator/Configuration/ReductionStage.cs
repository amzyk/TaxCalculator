using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Configuration
{
    public class ReductionStage
    {
        public decimal Step { get; set; }
        public decimal Amount { get; set; }
        public decimal CalculationAmount { get; set; }
        public decimal Division { get; set; }
    }
}
