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
        private TaxCalculationService _taxCalculationService;
        public TaxController(TaxCalculationService taxCalculationService)
        {
            _taxCalculationService = taxCalculationService;
        }

        [Route("taxes")]
        [HttpGet]
        public IActionResult GetTaxesForSalary([FromBody] SalaryInfo salary)
        {
            try
            {
                var taxQuotes = _taxCalculationService.CalculateTaxQuotes(salary.Value);
                return Ok(taxQuotes);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("insurance")]
        [HttpGet]
        public IActionResult GetInsuranceQuotes([FromBody] SalaryInfo salary)
        {
            try
            {
                var insuranceQuotes = _taxCalculationService.CalculateInsuranceQuotes(salary.Value);
                return Ok(insuranceQuotes);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("health")]
        [HttpGet]
        public IActionResult GetHealthInsuranceQuote([FromBody] SalaryInfo salary)
        {
            try
            {
                var healthInsuranceQuotes = _taxCalculationService.CalculateHealthInsuranceQuotes(salary.Value);
                return Ok(healthInsuranceQuotes);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
