using Domain.DTOs.Users;
using Domain.Entities.Companies;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        readonly IUserRepository _userRepository;
        readonly IAsyncRepository<Company> _companyRepository;

        public AddUserValidator(IUserRepository userRepository, IAsyncRepository<Company> companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(c => c.CompanyId).MustAsync(ValidateCompanyIsExist)
                 .WithErrorCode(ValidatorErrorCodes.BadRequest)
                 .WithMessage(c => ValidationErrorMessages.ErrorCompanyIsNotExist(c.CompanyId));

            RuleFor(c => c.Email).MustAsync(ValidateUserEmail)
                                 .WithErrorCode(ValidatorErrorCodes.BadRequest)
                                 .WithMessage(c => ValidationErrorMessages.ErrorEmailAlreadyTaken(c.Email)).DependentRules(() =>
                                 {
                                     RuleFor(c => c.Password).Must(ValidatiorExtentions.ValidatePassword)
                                                            .WithErrorCode(ValidatorErrorCodes.BadRequest)
                                                            .WithMessage(ValidationErrorMessages.ErrorInvalidPassword);
                                 });

        }

        public async Task<bool> ValidateUserEmail(string email, CancellationToken token)
        {
            return !(await _userRepository.IsExistAsync(x => x.Email == email));
        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }
    }
}
