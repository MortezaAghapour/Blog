using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Skills;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;

namespace Blog.Domain.Contracts.Repositories.Skills
{
    public interface ISkillRepository :IRepository<Skill>,IScopedLifeTime
    {
        
    }
}