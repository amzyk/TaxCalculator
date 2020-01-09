using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Configuration;
using TaxCalculator.DTO;

namespace TaxCalculator.Services
{
    public interface ITaxCalculationService
    {
        HealthInsuranceInfo CalculateHealthInsuranceQuotes(decimal salary);
        InsuranceInfo CalculateInsuranceQuotes(decimal salary);
        TaxInfo CalculateTaxQuotes(decimal salary);
    }
}
