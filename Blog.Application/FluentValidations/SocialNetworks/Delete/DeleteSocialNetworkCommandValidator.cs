using Blog.Application.Commands.SocialNetworks.Delete;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.SocialNetworks.Delete
{
    public class DeleteSocialNetworkCommandValidator  :AbstractValidator<DeleteSocialNetworkCommand>
    {
        public DeleteSocialNetworkCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(ValidationErrorResources.SocialNetworkIdDeleteIdRequired)
                .LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.SocialNetworkIdNotLessThanZero);
        }
    }
}