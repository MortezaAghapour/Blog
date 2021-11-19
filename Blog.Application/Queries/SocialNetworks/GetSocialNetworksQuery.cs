using System.Collections.Generic;
using Blog.Application.Dtos.SocialNetworks;
using MediatR;

namespace Blog.Application.Queries.SocialNetworks
{
    public class GetSocialNetworksQuery:IRequest<List<SocialNetworkDto>>
    {
        
    }
}