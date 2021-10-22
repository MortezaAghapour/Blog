using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Categories;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.Categories
{
    public interface ICategoryRepository:IRepository<Category> ,IScopedLifeTime
    {
        
    }
}