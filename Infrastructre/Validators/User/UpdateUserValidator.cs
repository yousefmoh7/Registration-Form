using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        readonly IUserRepository _userRepository;
        readonly ICompanyRepository _companyRepository;

        public UpdateUserValidator(IUserRepository userRepository,
            ICompanyRepository companyRepository
            )
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(c => c.Id).MustAsync(ValidateUserIsExist)
              .WithErrorCode(ValidatorErrorCodes.NotFound)
              .WithMessage(c => ValidationErrorMessages.ErrorUserIsNotExist(c.Id))
              .DependentRules(() =>
              {
                  RuleFor(c => c).MustAsync(ValidateUserEmail)
                         .WithErrorCode(ValidatorErrorCodes.BadRequest)
                         .WithMessage(c => ValidationErrorMessages.ErrorEmailAlreadyTaken(c.Email)).DependentRules(() =>
                         {
                             RuleFor(c => c.Password).Must(ValidatiorExtentions.ValidatePassword)
                                                    .WithErrorCode(ValidatorErrorCodes.BadRequest)
                                                    .WithMessage(ValidationErrorMessages.ErrorInvalidPassword);
                         });
              });

            RuleFor(c => c.Id).MustAsync(ValidateCompanyIsExist)
             .WithErrorCode(ValidatorErrorCodes.NotFound)
             .WithMessage(c => ValidationErrorMessages.ErrorCompanyIsNotExist(c.Id));

        }

        public async Task<bool> ValidateUserIsExist(int id, CancellationToken token)
        {
            return await _userRepository.IsExistAsync(x => x.Id == id);
        }

        public async Task<bool> ValidateUserEmail(UpdateUserRequest user, CancellationToken token)
        {
            return !(await _userRepository.IsExistAsync(x => x.Email == user.Email && x.Id!=user.Id));
        }

        public async Task<bool> ValidateCompanyIsExist(int id, CancellationToken token)
        {
            return await _companyRepository.IsExistAsync(x => x.Id == id);
        }
    }
}
