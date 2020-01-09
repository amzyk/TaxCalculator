using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Configuration;
using TaxCalculator.DTO;

namespace TaxCalculator
{
    public class TaxCalculationService
    {
        private Tax _taxConfig;
        private Insurance _insuranceConfig;
        private Reduction _reductionConfig;

        public TaxCalculationService(
            IOptions<Tax> taxConfig,
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig)
        {
            _taxConfig = taxConfig.Value;
            _insuranceConfig = insuranceConfig.Value;
            _reductionConfig = reductionConfig.Value;
        }

        public InsuranceInfo CalculateInsurance(decimal salary)
        {
            var annuarSalary = salary * 12;
            return new InsuranceInfo
            {

            };

            return null;
        }

        public HealthInsuranceInfo CalculateHealthInsurance(decimal salary)
        {
            return null;
        }

        public TaxInfo CalculateTax(SalaryInfo salary)
        {
            return null;
        }
    }
}
