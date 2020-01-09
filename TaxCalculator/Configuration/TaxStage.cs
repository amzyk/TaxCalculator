using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Configuration
{
    public class TaxStage
    {
        public decimal Rate { get; set; }
        public decimal Step { get; set; }
    }
}
