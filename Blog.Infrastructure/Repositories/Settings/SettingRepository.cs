using Blog.Domain.Contracts.Repositories.Settings;
using Blog.Domain.Entities.Settings;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.Settings
{
    public class SettingRepository  :Repository<Setting>,ISettingRepository
    {
        public SettingRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}