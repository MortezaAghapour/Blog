using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.SocialNetworks;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.SocialNetworks
{
    public interface ISocialNetworkRepository : IRepository<SocialNetwork>, IScopedLifeTime
    {

    }
}