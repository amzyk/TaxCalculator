using Microsoft.Extensions.Options;
using System;
using TaxCalculator.Configuration;
using TaxCalculator.DTO;

namespace TaxCalculator.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private Tax _taxConfig;
        private Reduction _reductionConfig;
        private Insurance _insuranceConfig;

        public TaxCalculationService(
            IOptions<Tax> taxConfig,
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig)
        {
            _taxConfig = taxConfig.Value;
            _reductionConfig = reductionConfig.Value;
            _insuranceConfig = insuranceConfig.Value;
        }

        public HealthInsuranceInfo CalculateHealthInsuranceQuotes(decimal salary)
        {
            var value = salary * _insuranceConfig.Health / 100;
            return new HealthInsuranceInfo
            {
                Value = Math.Round(value, 2, MidpointRounding.AwayFromZero)
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

            return new TaxInfo(Math.Round(resultQuote, 0, MidpointRounding.ToEven));
        }

        private decimal CalculateTaxBase(decimal basicQuote)
        {
            var firstStageQuote = _taxConfig.SecondStage.Step * _taxConfig.FirstStage.Rate / 100;
            var secondStageQuote = (_taxConfig.ThirdStage.Step - _taxConfig.SecondStage.Step) * _taxConfig.SecondStage.Rate / 100;
            if (basicQuote > _taxConfig.ThirdStage.Step)
            {
                var thirdStageQuote = (basicQuote - _taxConfig.SecondStage.Step) * _taxConfig.SecondStage.Rate / 100;
                return firstStageQuote + secondStageQuote + thirdStageQuote;
            }

            if (basicQuote > _taxConfig.SecondStage.Step)
            {
                secondStageQuote = (basicQuote - _taxConfig.SecondStage.Step) * _taxConfig.SecondStage.Rate / 100;
                return firstStageQuote + secondStageQuote;
            }

            return basicQuote * _taxConfig.FirstStage.Rate / 100;
        }

        private decimal GetReductionQuote(decimal salary)
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

        private decimal CalculateHealthInsuranceQuoteForReduction(decimal salary)
        {
            return salary * _insuranceConfig.HealthForReduction / 100;
        }
    }
}
