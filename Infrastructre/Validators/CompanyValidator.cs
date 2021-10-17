using API.DTOs.Compaines;
using FluentValidation;

namespace Infrastructre.Validators
{
    public class CompanyValidator : AbstractValidator<AddCompanyRequest>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address)
                   .Must(c => c.Length > 5)
                   .WithErrorCode("404")
                   .WithMessage("Length should be more than 5");
                   

        }
    }
}
