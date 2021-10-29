using Blog.Infrastructure.Identity.Dtos;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Infrastructure.Identity.FluentValidations
{
    public class LoginValidator:AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage(ValidationErrorResources.UserNameIsRequired);
            RuleFor(c => c.Password).NotEmpty().WithMessage(ValidationErrorResources.PasswordIsRequired);
        }
    }
}
