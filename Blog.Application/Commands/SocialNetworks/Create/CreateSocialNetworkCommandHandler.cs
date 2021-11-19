using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.SocialNetworks;
using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.SocialNetworks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Create
{
    public class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, SocialNetworkDto>
    {

        #region Fields

        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IUnitOfWork _unitOfWork;


        #endregion
        #region Constructors
        public CreateSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IUnitOfWork unitOfWork)
        {
            _socialNetworkRepository = socialNetworkRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion
        #region Methods
        public async Task<SocialNetworkDto> Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
        {
            var socialNetwork =await _socialNetworkRepository.Get(expression: c =>
                c.Name.Trim().ToLower()
                    .Equals(
                        request.Name.Trim().ToLower()) &&
                        c.Address.Trim().ToLower().Equals(request.Address.Trim().ToLower()), cancellationToken: cancellationToken);
            if (!(socialNetwork is null))
            {
                throw new AppException(ValidationErrorResources.TheSocialIsExist);
            }

            socialNetwork = request.Adapt<SocialNetwork>();
            await _socialNetworkRepository.Insert(socialNetwork, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return socialNetwork.Adapt<SocialNetworkDto>();
        }
        #endregion

    }
}                                                                                    