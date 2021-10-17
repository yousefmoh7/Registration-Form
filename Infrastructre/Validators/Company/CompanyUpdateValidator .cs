using Domain.Companies;
using Domain.DTOs.Compaines;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class CompanyUpdateValidator : AbstractValidator<UpdateCompanyRequest>
    {
        readonly IAsyncRepository<Company> _companyRepository;

        public CompanyUpdateValidator(IAsyncRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
             .WithErrorCode(ValidatorErrorCodes.NotFound)
             .WithMessage(c => ValidationErrorMessages.ErrorCompanyIsNotExist(c.Id)).DependentRules(()=>{

                 RuleFor(c => c).MustAsync(ValidateCompanyName)
                   .WithErrorCode(ValidatorErrorCodes.BadRequest)
                   .WithMessage(c => ValidationErrorMessages.ErrorCompanyNameAlreadyExist(c.Name))
                   .DependentRules(() =>
                   {

                     RuleFor(c => c.Address)
                    .Must(c => c.Length > 5)
                    .WithErrorCode(ValidatorErrorCodes.BadRequest)
                    .WithMessage(ValidationErrorMessages.ErrorCompanyAddress);
                   });
             });
        }

        public async Task<bool> ValidateCompanyName(UpdateCompanyRequest company, CancellationToken token)
        {
            var x= _companyRepository.IsExistAsync(x => x.Name == company.Name && x.Id != company.Id);

            return ! await _companyRepository.IsExistAsync(x => x.Name == company.Name && x.Id != company.Id);
        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }

    }
}
