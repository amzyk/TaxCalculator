using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Configuration;
using TaxCalculator.Services;

namespace TaxCalculatorTests
{
    [TestClass]
    public class TaxCalculationServiceTests
    {
        private TaxCalculationService taxCalculationService;
        public TaxCalculationServiceTests()
        {
            var config = InitConfiguration();
            var taxConfig = config.GetSection("Tax").Get<Tax>();
            var taxConfigOptions = Options.Create(taxConfig);

            var insuranceConfig = config.GetSection("Insurance").Get<Insurance>();
            var insuranceConfigOptions = Options.Create(insuranceConfig);

            var reductionConfig = config.GetSection("Reduction").Get<Reduction>();
            var reductionConfigOptions = Options.Create(reductionConfig);
            taxCalculationService = new TaxCalculationService(taxConfigOptions, insuranceConfigOptions, reductionConfigOptions);
        }

        [TestMethod]
        public void CalculateTax_FirstStage_CorrectValues()
        {
            var salary = 40000;
            var taxInfo = taxCalculationService.CalculateTaxQuotes(salary);

            Assert.IsNotNull(taxInfo);
            Assert.IsNotNull(taxInfo.Value);
            Assert.AreEqual(1023, taxInfo.Value);
        }

        [TestMethod]
        public void CalculateInsurance_FirstStage_CorrectValues()
        {
            var salary = 40000;
            var insuranceQuote = taxCalculationService.CalculateInsuranceQuotes(salary);

            Assert.IsNotNull(insuranceQuote);
            Assert.AreEqual(7808, insuranceQuote.Retirement);
            Assert.AreEqual(3200, insuranceQuote.Disability);
            Assert.AreEqual(980, insuranceQuote.Disease);
            Assert.AreEqual(668, insuranceQuote.Accidental);
            Assert.AreEqual(980, insuranceQuote.LaborFound);

            var insuranceSum = insuranceQuote.SumSocialQuotes();
            Assert.AreEqual(12656, insuranceSum);
        }

        [TestMethod]
        public void CalculateHealthInsurance_FirstStage_CorrectValues()
        {
            var salary = 40000;
            var healthInsuranceQuote = taxCalculationService.CalculateHealthInsuranceQuotes(salary);

            Assert.IsNotNull(healthInsuranceQuote);
            Assert.IsNotNull(healthInsuranceQuote.Value);
            Assert.AreEqual(3100, healthInsuranceQuote.Value);
        }

        [TestMethod]
        public void CalculateTax_SecondStage_CorrectValues()
        {
            var salary = 100000;
            var taxInfo = taxCalculationService.CalculateTaxQuotes(salary);

            Assert.IsNotNull(taxInfo);
            Assert.IsNotNull(taxInfo.Value);
            Assert.AreEqual(3785, taxInfo.Value);
        }

        [TestMethod]
        public void CalculateInsurance_SecondStage_CorrectValues()
        {
            var salary = 100000;
            var insuranceQuote = taxCalculationService.CalculateInsuranceQuotes(salary);

            Assert.IsNotNull(insuranceQuote);
            Assert.AreEqual(19520, insuranceQuote.Retirement);
            Assert.AreEqual(8000, insuranceQuote.Disability);
            Assert.AreEqual(2450, insuranceQuote.Disease);
            Assert.AreEqual(1670, insuranceQuote.Accidental);
            Assert.AreEqual(2450, insuranceQuote.LaborFound);

            var insuranceSum = insuranceQuote.SumSocialQuotes();
            Assert.AreEqual(31640, insuranceSum);
        }

        [TestMethod]
        public void CalculateHealthInsurance_SecondStage_CorrectValues()
        {
            var salary = 100000;
            var healthInsuranceQuote = taxCalculationService.CalculateHealthInsuranceQuotes(salary);

            Assert.IsNotNull(healthInsuranceQuote);
            Assert.IsNotNull(healthInsuranceQuote.Value);
            Assert.AreEqual(9000, healthInsuranceQuote.Value);
        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("taxTest.json")
                .Build();
            return config;
        }
    }
}
