using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.EndPoint.Infrastructure.Extensions.ModelsStates
{
    public static class ModelStateExtensions
    {
        public static List<string> GetModelStateErrors(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage).ToList();
        }
    }
}
