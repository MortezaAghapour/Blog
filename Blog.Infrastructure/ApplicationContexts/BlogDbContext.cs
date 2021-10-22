using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;
using Blog.Infrastructure.Contracts.Helpers;
using Blog.Infrastructure.Extensions.DbContexts;
using Blog.Infrastructure.Identity.Entities;
using Blog.Shared.Markers.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Infrastructure.ApplicationContexts
{
    public class BlogDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        #region Constrcutors
        public BlogDbContext(DbContextOptions options) : base(options)
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
            //register all configurations
            builder.RegisterConfigurations(GetType().Assembly);
            //add restrict delete behavior convention
            builder.AddRestrictDeleteBehaviorConvention();
            //pluralize all table names
            builder.PluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            SetAuditingPropertyValues();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            SetAuditingPropertyValues();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }
        #endregion

        #region Utilities
        private void SetAuditingPropertyValues()
        {
            var webHelper = this.GetService<IWebHelper>();
            var workContext = this.GetService<IWorkContext>();
            ChangeTracker.SetAuditableEntityPropertyValues(webHelper,workContext);
        }
        #endregion

    }
}