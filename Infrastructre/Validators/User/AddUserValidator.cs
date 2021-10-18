using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Shared;
using Domain.Users;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        readonly IAsyncRepository<User> _userRepository;
     
        public AddUserValidator(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(c => c.Email).MustAsync(ValidateUserEmail)
                                 .WithErrorCode(ValidatorErrorCodes.BadRequest)
                                 .WithMessage(c => ValidationErrorMessages.ErrorEmailAlreadyTaken(c.Email)).DependentRules(()=>
                                 {
                                     RuleFor(c => c.Password).Must(ValidatiorExtentions.ValidatePassword)
                                                            .WithErrorCode(ValidatorErrorCodes.BadRequest)
                                                            .WithMessage(ValidationErrorMessages.ErrorInvalidPassword);
                                 });

        }

        public async Task<bool> ValidateUserEmail(string email, CancellationToken token)
        {
            return !await _userRepository.IsExistAsync(x => x.Email == email);
        }
    }
}
