using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.EndPoint.Infrastructure.Extensions.Validators
{
    public static class ValidatorExtensions
    {

        public static async Task<List<string>> FluentValidate<T>(this IValidator validator, T entity,CancellationToken cancellationToken=default)
        {
            var errors = new List<string>();
            var context = new ValidationContext<T>(entity);
            var result =await validator.ValidateAsync(context,cancellationToken);
            if (result.Errors.Count > 0)
            {
                errors = result.Errors.Select(c => c.ErrorMessage).ToList();
            }

            return errors;
        }
    }
}
