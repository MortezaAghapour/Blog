using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.SocialNetworks;
using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Mapster;
using MediatR;

namespace Blog.Application.Queries.SocialNetworks
{
    public class GetSocialNetworkQueryHandler : IRequestHandler<GetSocialNetworksQuery, List<SocialNetworkDto>>
    {
        #region Fields
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        #endregion
        #region Constructors
        public GetSocialNetworkQueryHandler(ISocialNetworkRepository socialNetworkRepository)
        {
            _socialNetworkRepository = socialNetworkRepository;
        }
        #endregion
        #region Methods
        public async Task<List<SocialNetworkDto>> Handle(GetSocialNetworksQuery request, CancellationToken cancellationToken)
        {
            var sliders = await _socialNetworkRepository.GetAll(asNoTracking: true, cancellationToken: cancellationToken);
            return sliders.Adapt<List<SocialNetworkDto>>();
        }
        #endregion

    }
}