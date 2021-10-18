using Domain.Companies;
using Domain.DTOs.Compaines;
using Domain.DTOs.Companies;
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
        public static string ErrorCompanyIsNotExist(int id) => $"Company with id :{id} does not exist ";

        public CompanyDeleteValidator(IAsyncRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
              .WithErrorCode(ValidatorErrorCodes.NotFound)
              .WithMessage(c => ErrorCompanyIsNotExist(c.Id))
               .DependentRules(() =>
               {
                   RuleFor(c => c.Id).MustAsync(ValidateCompanyHaveUsers)
                       .WithErrorCode(ValidatorErrorCodes.BadRequest)
                       .WithMessage(c => ErrorCompanyIsNotExist(c.Id));
               });

               }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }

        public async Task<bool> ValidateCompanyHaveUsers(int id, CancellationToken token)
        {
            var xx = (await _companyRepository.GetAsyncById(id));
             return (await _companyRepository.GetAsyncById(id)).Users.Count > 0;            
        }



    }
}
