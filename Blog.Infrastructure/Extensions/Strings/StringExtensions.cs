using Pluralize.NET.Core;

namespace Blog.Infrastructure.Extensions.Strings
{
    public static class StringExtensions
    {
        public static string Pluralize(this string key)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Pluralize(key);

        }
        public static string Singularize(this string key)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Singularize(key);

        }

    }
}