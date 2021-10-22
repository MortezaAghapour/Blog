using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Posts;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.Posts
{
    public interface IPostRepository  :IRepository<Post> ,IScopedLifeTime
    {
        
    }
}