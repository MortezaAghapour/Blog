using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Dtos.AppSettings.Cors;
using Blog.EndPoint.Infrastructure.Filters;
using Blog.EndPoint.Infrastructure.Schedules;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Blog.EndPoint.Infrastructure.Extensions.Startup
{
    public static class ConfigureExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, IConfiguration configuration)
        {
            //custom exception handler
            //application.UseCustomExceptionHandler();
            application.UseResponseCompression();
            //use static files feature
            application.UseStaticFiles();

            application.UseHsts();
            application.UseCookiePolicy();
            application.UseRouting();
            //authentication
            application.UseAuthentication();
            application.UseAuthorization();
            //add cors
            application.UseCors("BlogPolicy");
          
            
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
            application.UseHangFire();
        }

        #region Hangfire Config
        public static void UseHangFire(this IApplicationBuilder application)
        {
            application.UseHangfireDashboard("/HangFire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() },
                StatsPollingInterval = 30000
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            HangFireSchedules.HangFireJobRecurring();
        }
        #endregion

        #region Custom Cors

//        public static void UseCustomCors(this IApplicationBuilder application, IConfiguration configuration)
//        {
//            application.UseCors(options =>
//            {
//                var corsConfiguration = configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
//
//
//                if (corsConfiguration.AllowAllOrigins)
//                {
//                    options.AllowAnyOrigin();
//                }
//                else
//                {
//                    var allCors = corsConfiguration.Origins.Split(",");
//                    options.WithOrigins(allCors);
//                }
//
//                if (corsConfiguration.AllowAllHeaders)
//                {
//                    options.AllowAnyMethod();
//                }
//                else
//                {
//                    var allHeaders = corsConfiguration.Headers.Split(",");
//                    options.WithHeaders(allHeaders);
//                }
//
//                if (corsConfiguration.AllowAllMethods)
//                {
//                    options.AllowAnyMethod();
//                }
//                else
//                {
//                    var allMethods = corsConfiguration.Methods.Split(",");
//                    options.WithMethods(allMethods);
//                }
//            });
//        }
        #endregion

    }
}
