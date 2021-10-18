using Domain.DTOs.Companies;
using Domain.Entities.Companies;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class CompanyGetValidator : AbstractValidator<CompanyBaseRequest>
    {
        readonly IAsyncRepository<Company> _companyRepository;
        public static string ErrorCompanyIsNotExist(int id) => $"Company with id :{id} does not exist ";

        public CompanyGetValidator(IAsyncRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
              .WithErrorCode(ValidatorErrorCodes.BadRequest)
              .WithMessage(c => ErrorCompanyIsNotExist(c.Id));

        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }

    }
}
