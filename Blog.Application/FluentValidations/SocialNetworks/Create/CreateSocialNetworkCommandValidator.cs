using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.SocialNetworks.Create;
using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.SocialNetworks.Create
{
    public class CreateSocialNetworkCommandValidator :AbstractValidator<CreateSocialNetworkCommand>
    {
        #region Fields

        private readonly ISocialNetworkRepository _socialNetworkRepository;
        #endregion
        #region Constructors

        public CreateSocialNetworkCommandValidator(ISocialNetworkRepository socialNetworkRepository)
        {
            _socialNetworkRepository = socialNetworkRepository;

            RuleFor(c => c.Name).MaximumLength(500).WithMessage(ValidationErrorResources.SocialNetworkNameMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SocialNetworkNameIsRequired);   
            RuleFor(c => c.Icon).MaximumLength(500).WithMessage(ValidationErrorResources.SocialNetworkIconMaxLength);     
            RuleFor(c => c.Color).MaximumLength(50).WithMessage(ValidationErrorResources.SocialNetworkColorMaxLength);  
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage(ValidationErrorResources.SocialAddressIsRequired)
                .MustAsync(async (model, address, cancellation) => await CheckExistSocial(model, address, cancellation)).WithMessage(ValidationErrorResources.TheSocialIsExist);
        }
        #endregion
        #region Methods
        public async Task<bool> CheckExistSocial(CreateSocialNetworkCommand model, string address, CancellationToken cancellationToken = default)
        {
            return (await _socialNetworkRepository.Get(expression: c => c.Address.Trim().ToLower().Equals(address.Trim().ToLower())&& c.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()), cancellationToken: cancellationToken)) is null;
        }
        #endregion
    }
}