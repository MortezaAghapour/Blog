using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blog.Domain.Entities.Base;
using Blog.Infrastructure.Contracts.Helpers;
using Blog.Infrastructure.Extensions.Strings;
using Blog.Shared.Consts.Auditing;
using Blog.Shared.Markers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog.Infrastructure.Extensions.DbContexts
{
    public static class DbContextExtensions
    {

        #region Register Entities
        public static void RegisterEntities<TType>(this ModelBuilder builder, params Assembly[] assemblies)
        {

            //GetExportedTypes():Gets the public types defined in this assembly that are visible outside the assembly
            var types = assemblies.SelectMany(c => c.GetExportedTypes()).Where(c =>
                c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TType).IsAssignableFrom(c));
            foreach (var type in types)
            {
                builder.Entity(type);
            }
        }
        #endregion
        #region Register Configurations
        public static void RegisterConfigurations(this ModelBuilder builder, Assembly assembly)
        {
            builder.ApplyConfigurationsFromAssembly(assembly);
        }
        #endregion
        #region Restrict Delete Behavior Convention
        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder builder)
        {
            var cascadeFks = builder.Model.GetEntityTypes().SelectMany(c => c.GetForeignKeys())
                .Where(c => c.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        #endregion
        #region Pluralizing Table Name Convention
        public static void PluralizingTableNameConvention(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().Pluralize());
            }
        }
        #endregion
        #region Get Include Path
        public static IEnumerable<string> GetIncludePaths(this IModel model, Type clrEntityType)
        {
            var entityType = model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigations = new List<INavigation>();
                if (entityType != null)
                {
                    foreach (var navigation in entityType.GetNavigations())
                    {
                        if (includedNavigations.Add(navigation))
                            entityNavigations.Add(navigation);
                    }

                    if (entityNavigations.Count == 0)
                    {
                        if (stack.Count > 0)
                            yield return string.Join(".", stack.Reverse().Select(e => e.Current?.Name));
                    }
                    else
                    {
                        foreach (var navigation in entityNavigations)
                        {
                            var inverseNavigation = navigation.Inverse;
                            if (inverseNavigation != null)
                                includedNavigations.Add(inverseNavigation);
                        }

                        stack.Push(entityNavigations.GetEnumerator());
                    }

                    while (stack.Count > 0 && !stack.Peek().MoveNext())
                        stack.Pop();
                    if (stack.Count == 0) break;
                }

                entityType = stack.Peek().Current?.TargetEntityType;
            }
        }
        #endregion

        #region Set Auditable Entity Property Values
        public static void SetAuditableEntityPropertyValues( this ChangeTracker changeTracker,IWebHelper webHelper, IWorkContext workContext)
        {

            var userAgent = webHelper.GetUserAgent();
            var userIp = webHelper.GetIpAddress();
            var now = DateTime.UtcNow;
            var currentUser = workContext.CurrentUser;
            var userInfo = new AuditUserInfo
            {
                IpAddress = userIp,
                UserAgent = userAgent,
                UserId = currentUser.Id,
                UserName = currentUser.UserName
            };


            var modifiedEntries = changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property(AuditConsts.ModifyDate).CurrentValue = now;
                modifiedEntry.Property(AuditConsts.UserInfo).CurrentValue = userInfo;
            }

            var addedEntries = changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Property(AuditConsts.CreateDate).CurrentValue = now;
                addedEntry.Property(AuditConsts.UserInfo).CurrentValue = userInfo;
            }
            var deletedEntries = changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Added);
            foreach (var deletedEntry in deletedEntries)
            {
                deletedEntry.Property(AuditConsts.DeleteDate).CurrentValue = now;
                deletedEntry.Property(AuditConsts.UserInfo).CurrentValue = userInfo;
            }
        }
        #endregion


    }
}