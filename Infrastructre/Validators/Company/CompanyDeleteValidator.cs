using Domain.DTOs.Companies;
using Domain.Entities.Companies;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class CompanyDeleteValidator : AbstractValidator<DeleteCompanyRequest>
    {
        readonly IAsyncRepository<Company> _companyRepository;

        public CompanyDeleteValidator(IAsyncRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
              .WithErrorCode(ValidatorErrorCodes.NotFound)
              .WithMessage(c => ValidationErrorMessages.ErrorCompanyIsNotExist(c.Id))
               .DependentRules(() =>
               {
                   RuleFor(c => c.Id).MustAsync(ValidateCompanyHaveUsers)
                       .WithErrorCode(ValidatorErrorCodes.BadRequest)
                       .WithMessage(c => ValidationErrorMessages.ErrorCompanyHaveUsers);
               });

        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }

        public async Task<bool> ValidateCompanyHaveUsers(int id, CancellationToken token)
        {
            var xx = (await _companyRepository.GetAsyncById(id, x => x.Users)).Users.Count;
            return (await _companyRepository.GetAsyncById(id, x => x.Users)).Users.Count == 0;
        }
    }
}
