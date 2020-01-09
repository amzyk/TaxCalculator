using Microsoft.Extensions.Options;
using TaxCalculator.Configuration;

namespace TaxCalculator.Services
{
    public class ThirdTaxStageCalculationService : BaseTaxCalculationService
    {
        private Tax _taxConfig;

        public ThirdTaxStageCalculationService(
            IOptions<Tax> thirdTaxStage,
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig
            )
            : base(insuranceConfig, reductionConfig)
        {
            _taxConfig = thirdTaxStage.Value;
        }

        public override decimal CalculateTaxBase(decimal basicQuote)
        {
            var firstStageQuote = _taxConfig.SecondStage.Step * _taxConfig.FirstStage.Rate;
            var secondStageQuote = _taxConfig.ThirdStage.Step * _taxConfig.SecondStage.Rate;
            var resultQuote = firstStageQuote + secondStageQuote + (basicQuote - _taxConfig.SecondStage.Step) * _taxConfig.SecondStage.Rate / 100;
            return resultQuote;
        }
    }
}
