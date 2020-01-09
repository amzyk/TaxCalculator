namespace TaxCalculator.DTO
{
    public class TaxInfo
    {
        public TaxInfo(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }
    }
}
