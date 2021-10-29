using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.Identity.Dtos;
using Blog.Infrastructure.Identity.Services.Users;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Infrastructure.Identity.FluentValidations
{
    public class RegisterValidator  :AbstractValidator<RegisterDto>
    {
        #region Fields

        private readonly IUserService _userService;
        #endregion
        public RegisterValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(c => c.UserName).NotEmpty().WithMessage(ValidationErrorResources.UserNameIsRequired)
                .MustAsync(CheckExistUserName).WithMessage(ValidationErrorResources.UserNameIsExist);
            RuleFor(c => c.Password).NotEmpty().WithMessage(ValidationErrorResources.PasswordIsRequired);

        }

        #region Methods
        public async Task<bool> CheckExistUserName(string userName, CancellationToken cancellationToken)
        {
            return (await _userService.GetUserByUserName(userName)) is null;
        }
        #endregion
    }
}