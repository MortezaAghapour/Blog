using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Sliders;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.Sliders
{
    public interface ISliderRepository:IRepository<Slider>,IScopedLifeTime
    {
        
    }
}