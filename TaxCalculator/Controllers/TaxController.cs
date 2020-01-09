using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using TaxCalculator.Configuration;
using TaxCalculator.DTO;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    [Route("/api/tax")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private Func<decimal, Tax, ITaxCalculationService> _taxCalculationServiceResolver;
        private Tax _taxConfig;

        public TaxController(Func<decimal, Tax, ITaxCalculationService> taxCalculationServiceResolver, IOptions<Tax> taxConfigOptions)
        {
            _taxCalculationServiceResolver = taxCalculationServiceResolver;
            _taxConfig = taxConfigOptions.Value;
        }

        [HttpPost]
        public IActionResult PostSalary()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetTaxesForSalary([FromBody] SalaryInfo salary)
        {
            var taxCalculationService = _taxCalculationServiceResolver(salary.Value, _taxConfig);
            taxCalculationService.CalculateTaxQuotes(salary.Value);
            return Ok();
        }
    }
}
