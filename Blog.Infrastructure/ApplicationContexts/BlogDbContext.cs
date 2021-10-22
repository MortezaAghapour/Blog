using Blog.Domain.Entities.Base;
using Blog.Infrastructure.Extensions.DbContexts;
using Blog.Infrastructure.Identity.Entities;
using Blog.Shared.Markers.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.ApplicationContexts
{
    public class BlogDbContext  :IdentityDbContext<AppUser,AppRole,long,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>
    {
        #region Constrcutors
        public BlogDbContext(DbContextOptions options):base(options)
        {
            
        }
        #endregion
        #region Overrides

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var assemblies = typeof(Entity).Assembly;
            //register all entities
            builder.RegisterEntities<IEntity>(assemblies);
        }

        #endregion

    }
}