using Microsoft.Extensions.Options;
using System;
using TaxCalculator.Configuration;
using TaxCalculator.DTO;

namespace TaxCalculator.Services
{
    public class BaseTaxCalculationService : ITaxCalculationService
    {
        private Reduction _reductionConfig;
        private Insurance _insuranceConfig;

        public BaseTaxCalculationService(
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig)
        {
            _reductionConfig = reductionConfig.Value;
            _insuranceConfig = insuranceConfig.Value;
        }

        public HealthInsuranceInfo CalculateHealthInsuranceQuotes(decimal salary)
        {
            return new HealthInsuranceInfo
            {
                Value = salary * _insuranceConfig.Health / 100,
            };
        }

        public InsuranceInfo CalculateInsuranceQuotes(decimal salary)
        {
            return new InsuranceInfo
            {
                Retirement = salary * _insuranceConfig.Retirement / 100,
                Disease = salary * _insuranceConfig.Disease / 100,
                Disability = salary * _insuranceConfig.Disability / 100,
                Accidental = salary * _insuranceConfig.Accidental / 100,
                LaborFound = salary * _insuranceConfig.LaborFound / 100,
            };
        }

        public TaxInfo CalculateTaxQuotes(decimal salary)
        {
            var healthInsuranceQuotes = CalculateHealthInsuranceQuoteForReduction(salary);
            var insuranceQuotes = CalculateInsuranceQuotes(salary);

            var basicQuote = Math.Ceiling(salary - insuranceQuotes.SumSocialQuotes());
            var taxBase = CalculateTaxBase(basicQuote);
            var resultQuote = taxBase - healthInsuranceQuotes - GetReductionQuote(salary);
            return new TaxInfo(resultQuote);
        }

        protected decimal GetReductionQuote(decimal salary)
        {
            if (salary > _reductionConfig.FourthStage.Step)
            {
                return 0m;
            }

            if (salary > _reductionConfig.ThirdStage.Step)
            {
                var firstReduction = _reductionConfig.FourthStage.CalculationAmount * (salary - _reductionConfig.ThirdStage.Step);
                return _reductionConfig.FourthStage.Amount - firstReduction / _reductionConfig.FourthStage.Division;
            }

            if (salary > _reductionConfig.SecondStage.Step)
            {
                return _reductionConfig.ThirdStage.Amount;
            }

            if (salary > _reductionConfig.FirstStage.Step)
            {
                var firstReduction = _reductionConfig.SecondStage.CalculationAmount * (salary - _reductionConfig.FirstStage.Step);
                return _reductionConfig.SecondStage.Amount - firstReduction / _reductionConfig.SecondStage.Division;
            }

            return _reductionConfig.FirstStage.Amount;
        }

        protected decimal CalculateHealthInsuranceQuoteForReduction(decimal salary)
        {
            return salary * _insuranceConfig.HealthForReduction / 100;
        }

        public virtual decimal CalculateTaxBase(decimal basicQuote)
        {
            throw new NotImplementedException("Method should be overriden in derived class");
        }
    }
}
