using Blog.Infrastructure.Contracts.Helpers;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.EndPoint.Infrastructure.Filters
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {

            var httpContext = context.GetHttpContext();
            // var cancellationToke = httpContext.RequestAborted;
            var workContext = httpContext.RequestServices.GetService<IWorkContext>();
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity.IsAuthenticated;
            var logged = !(workContext.CurrentUser is null);
            return logged;
        }


    }
}
