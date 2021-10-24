using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Domain.Entities.Skills;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.Skills
{
    public class SkillRepository:Repository<Skill>,ISkillRepository
    {
        public SkillRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}