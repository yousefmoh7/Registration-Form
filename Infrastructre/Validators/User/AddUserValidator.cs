using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Shared;
using Domain.Users;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public static string ErrorEmailAlreadyTaken(string email) => $"User with email : {email} already taken.";

        readonly IAsyncRepository<User> _userRepository;
        public AddUserValidator(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(c => c.Email).MustAsync(ValidateUserEmail)
           .WithErrorCode(ValidatorErrorCodes.BadRequest)
           .WithMessage(c => ValidationErrorMessages.ErrorEmailAlreadyTaken(c.Email));

        }

        public async Task<bool> ValidateUserEmail(string email, CancellationToken token)
        {
            return !await _userRepository.IsExistAsync(x => x.Email == email);
        }
    }
}
