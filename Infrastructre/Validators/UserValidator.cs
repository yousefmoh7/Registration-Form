using API.DTOs.Users;
using FluentValidation;
using Infrastructre.Services.Users;

namespace Infrastructre.Validators
{
    public class UserValidator : AbstractValidator<AddUserRequest>
    {
        readonly IUserService _userService;
        public UserValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Password)
                   .Must(c => c.Length > 8);

        }
    }
}
