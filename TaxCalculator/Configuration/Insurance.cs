using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Configuration
{
    public class Insurance
    {
        public decimal Retirement { get; set; }
        public decimal Disability { get; set; }
        public decimal Disease { get; set; }
        public decimal Accidental { get; set; }
        public decimal LaborFound { get; set; }
        public decimal Health { get; set; }
        public decimal HealthForReduction { get; set; }
    }
}
