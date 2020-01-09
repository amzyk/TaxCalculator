using Microsoft.Extensions.Options;
using TaxCalculator.Configuration;

namespace TaxCalculator.Services
{
    public class FirstTaxStageCalculationService : BaseTaxCalculationService
    {
        private TaxStage _firstTaxStage;

        public FirstTaxStageCalculationService(
            IOptions<Tax> firstTaxStage,
            IOptions<Insurance> insuranceConfig,
            IOptions<Reduction> reductionConfig
            )
            : base(insuranceConfig, reductionConfig)
        {
            _firstTaxStage = firstTaxStage.Value.FirstStage;
        }
               
        public override decimal CalculateTaxBase(decimal basicQuote)
        {
            return basicQuote * _firstTaxStage.Rate / 100;
        }
    }
}
