using Domain.Companies;
using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Shared;
using Domain.Users;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        readonly IAsyncRepository<User> _userRepository;
        readonly IAsyncRepository<Company> _companyRepository;

        public UpdateUserValidator(IAsyncRepository<User> userRepository,
            IAsyncRepository<Company> companyRepository
            )
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(c => c.Id).MustAsync(ValidateUserIsExist)
              .WithErrorCode(ValidatorErrorCodes.NotFound)
              .WithMessage(c => ValidationErrorMessages.ErrorUserIsNotExist(c.Id))
              .DependentRules(() =>
              {
                  RuleFor(c => c.Email).MustAsync(ValidateUserEmail)
                     .WithErrorCode(ValidatorErrorCodes.BadRequest)
                     .WithMessage(c => ValidationErrorMessages.ErrorEmailAlreadyTaken(c.Email));

              });

            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
             .WithErrorCode(ValidatorErrorCodes.NotFound)
             .WithMessage(c => ValidationErrorMessages.ErrorCompanyIsNotExist(c.Id));

        }

        public async Task<bool> ValidateUserIsExist(int id, CancellationToken token)
        {
            return await _userRepository.IsExistAsync(x => x.Id == id);
        }

        public async Task<bool> ValidateUserEmail(string email, CancellationToken token)
        {
            return await _userRepository.IsExistAsync(x => x.Email == email);
        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }
    }
}
