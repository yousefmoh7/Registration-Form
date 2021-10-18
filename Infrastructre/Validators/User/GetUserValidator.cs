using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Shared;
using Domain.Users;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructre.Validators
{
    public class GetUserValidator : AbstractValidator<UserBaseRequest>
    {
        readonly IAsyncRepository<User> _userRepository;
        public GetUserValidator(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(c => c.Id).MustAsync(ValidateUserIsExist)
                   .WithErrorCode(ValidatorErrorCodes.NotFound)
                   .WithMessage(c => ValidationErrorMessages.ErrorUserIsNotExist(c.Id));
        }

        public async Task<bool> ValidateUserIsExist(int id, CancellationToken token)
        {
            return await _userRepository.IsExistAsync(x => x.Id == id);
        }

    }
}
