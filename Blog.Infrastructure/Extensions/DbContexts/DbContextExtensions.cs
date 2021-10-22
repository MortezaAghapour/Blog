using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog.Infrastructure.Extensions.DbContexts
{
    public static class DbContextExtensions
    {
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
        public static IEnumerable<string> GetIncludePaths(this IModel model,Type clrEntityType)
        {
            var entityType = model.FindEntityType(clrEntityType);
            var includedNavigations= new HashSet<INavigation>();
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
    }
}