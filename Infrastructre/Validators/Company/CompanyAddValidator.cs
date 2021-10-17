using Domain.Companies;
using Domain.DTOs.Compaines;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class CompanyAddValidator : AbstractValidator<AddCompanyRequest>
    {
        readonly IAsyncRepository<Company> _companyRepository;

        public CompanyAddValidator(IAsyncRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
            RuleFor(c => c.Name).MustAsync(ValidateCompany)
                .WithErrorCode(ValidatorErrorCodes.BadRequest)
                .WithMessage(c => ValidationErrorMessages.ErrorCompanyNameAlreadyExist(c.Name));

            RuleFor(c => c.Address)
                   .Must(c => c.Length > 5)
                   .WithErrorCode(ValidatorErrorCodes.BadRequest)
                   .WithMessage(ValidationErrorMessages.ErrorCompanyAddress);

        }

        public async Task<bool> ValidateCompany(string companyName, CancellationToken token)
        {
            return !await _companyRepository.IsExistAsync(x => x.Name == companyName);
        }
    }
}
