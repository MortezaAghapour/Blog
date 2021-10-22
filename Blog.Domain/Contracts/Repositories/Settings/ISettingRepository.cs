using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Settings;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.Settings
{
    public interface ISettingRepository  :IRepository<Setting>,IScopedLifeTime
    {
        
    }
}