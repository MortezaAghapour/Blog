using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.SocialNetworks;
using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using FluentValidation;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Update
{
    public class UpdateSocialNetworkCommandHandler : IRequestHandler<UpdateSocialNetworkCommand, SocialNetworkDto>
    {
        #region Fields

        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion
        #region Constructors
        public UpdateSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IUnitOfWork unitOfWork)
        {
            _socialNetworkRepository = socialNetworkRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<SocialNetworkDto> Handle(UpdateSocialNetworkCommand request, CancellationToken cancellationToken)
        {
            var socialNetwork = await _socialNetworkRepository.GetById(request.Id, cancellationToken);
            if (socialNetwork is null)
            {
                throw new NotFoundException($"The SocialNetwork Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            var checkName = await _socialNetworkRepository.Get(
                expression: c => c.Name.Trim().ToLower().Equals(request.Name.Trim().Trim())&&c.Address.Trim().ToLower().Equals(request.Address.Trim().ToLower()) && !c.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            if (!(checkName is null))
            {
                throw new ValidationException(ValidationErrorResources.TheSocialIsExist);
            }

            socialNetwork.Address = request.Address;
            socialNetwork.Name = request.Name;
            socialNetwork.Color = request.Color;
            socialNetwork.Icon = request.Icon;

            _socialNetworkRepository.Update(socialNetwork);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return socialNetwork.Adapt<SocialNetworkDto>();
        }
        #endregion

    }
}