using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Blog.Domain.Entities.SocialNetworks;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.SocialNetworks
{
    public class SocialNetworkRepository :Repository<SocialNetwork>,ISocialNetworkRepository
    {
        public SocialNetworkRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}