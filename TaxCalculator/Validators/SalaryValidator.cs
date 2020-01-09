using FluentValidation;
using TaxCalculator.DTO;

namespace TaxCalculator.Validators
{
    public class SalaryValidator : AbstractValidator<SalaryInfo>
    {
        public SalaryValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .WithMessage("The salary can not be null.");
            RuleFor(request => request.Value)
                .NotNull()
                .WithMessage("The salary value can not be null.");
            RuleFor(request => request.Value)
                .NotEqual(0m)
                .WithMessage("The salary value can not be 0.");
            RuleFor(request => request.Value)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("The salary value can not be smaller than 0.");
        }
    }
}
