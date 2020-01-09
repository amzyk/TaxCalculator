using Microsoft.Extensions.Options;
using TaxCalculator.Configuration;

namespace TaxCalculator.Services
{
    public class SecondTaxStageCalculationService : BaseTaxCalculationService
    {
        private Tax _taxConfig;

        public SecondTaxStageCalculationService(
            IOptions<Tax> secondTaxStage,
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig
            )
            :base(insuranceConfig, reductionConfig)
        {
            _taxConfig = secondTaxStage.Value;
        }

        public override decimal CalculateTaxBase(decimal basicQuote)
        {
            var firstStageQuote = _taxConfig.SecondStage.Step * _taxConfig.FirstStage.Rate;
            var resultQuote = firstStageQuote + (basicQuote - _taxConfig.SecondStage.Step)  * _taxConfig.SecondStage.Rate / 100;
            return resultQuote;
        }
    }
}
