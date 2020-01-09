namespace TaxCalculator.DTO
{
    public class InsuranceInfo
    {
        public decimal Retirement { get; set; }
        public decimal Disability { get; set; }
        public decimal Disease { get; set; }
        public decimal Accidental { get; set; }
        public decimal LaborFound { get; set; }

        public decimal SumSocialQuotes()
        {
            return Retirement + Disability + Disease + Accidental;
        }
    }
}
