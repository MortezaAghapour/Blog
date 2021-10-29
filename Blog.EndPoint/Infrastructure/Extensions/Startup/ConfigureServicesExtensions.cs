using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Blog.Application.Dtos.AppSettings.Cors;
using Blog.Application.Dtos.AppSettings.Identity;
using Blog.Application.Dtos.AppSettings.Jwt;
using Blog.Application.Dtos.Base;
using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Identity.Entities;
using Blog.Infrastructure.Identity.Services.Users;
using Blog.Infrastructure.Repositories.Base;
using Blog.Infrastructure.UnitOfWorks;
using Blog.Shared.Enums.Exceptions;
using Blog.Shared.Exceptions;
using Blog.Shared.Extensions.Identity;
using Blog.Shared.Helpers;
using Blog.Shared.Markers.Configurations;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Blog.Shared.Markers.Entities;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MediatR;

namespace Blog.EndPoint.Infrastructure.Extensions.Startup
{
    public static class ConfigureServicesExtensions
    {
        public static void DependencyRegistrar(this ContainerBuilder containerBuilder)
        {

            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        



          
            var domainAssembly = typeof(IEntity).Assembly;
            var applicationAssembly = typeof(EntityDto).Assembly;
            var infrastructureAssembly = typeof(BlogDbContext).Assembly;
            var endPointAssembly = typeof(Program).Assembly;


            containerBuilder.RegisterAssemblyTypes( domainAssembly, applicationAssembly, infrastructureAssembly, endPointAssembly)
                .AssignableTo<IScopedLifeTime>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(domainAssembly, applicationAssembly, infrastructureAssembly, endPointAssembly)
                .AssignableTo<ITransientLifeTime>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes( domainAssembly, applicationAssembly, infrastructureAssembly, endPointAssembly)
                .AssignableTo<ISingletonLifeTime>()
                .AsImplementedInterfaces()
                .SingleInstance();


        }
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //register all appSetting Configurations
            services.AddAppSettingConfigurations(configuration);
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //register http client
            services.AddHttpClient();

            //add DbContext Configurations
            services.AddDbContextConfiguration(configuration);
            services.AddIdentityConfiguration(configuration);
            services.AddEfServices();
            //add Response Compression
            services.AddResponseCompressionConfigurations();
            //add mediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //Add Cors
            services.AddCustomCors(configuration);
         
            services.AddControllers().AddFluentValidation(
                options =>
                {
                    options.RegisterValidatorsFromAssembly(typeof(EntityDto).Assembly);
                });
            services.AddAuthorization();
            services.AddHangFireConfig(configuration);
            services.AddJwtAuthentication(configuration);
        }
        #region Configure Configs

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services,
            IConfiguration configuration) where TConfig : class, IAppSetting, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        #endregion
        #region Add AppSetting Configurations

        public static void AddAppSettingConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureStartupConfig<IdentityConfiguration>(configuration.GetSection(nameof(IdentityConfiguration)));
            services.ConfigureStartupConfig<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
            
        }
        #endregion
        #region Ef Services

        public static void AddEfServices(this IServiceCollection services)
        {
            //add EF services
            services.AddEntityFrameworkSqlServer();

        }
        #endregion
        #region Response Compressions Configurations

        public static void AddResponseCompressionConfigurations(this IServiceCollection services)
        {
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
            });
        }
        #endregion
        #region DbContextConfigurations

        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContextPool<BlogDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString, assembly => assembly.MigrationsAssembly
                    (typeof(BlogDbContext).Assembly.FullName))
                    .ConfigureWarnings(w => w.Log(CoreEventId.ManyServiceProvidersCreatedWarning));
            });
            services.AddDbContext<BlogDbContext>();

        }

        #endregion
        #region IdentityConfigurations

        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var userConfiguration = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = userConfiguration.PasswordRequireDigit;
                options.Password.RequireLowercase = userConfiguration.PasswordRequireLowercase;
                options.Password.RequireNonAlphanumeric = userConfiguration.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = userConfiguration.PasswordRequireUppercase;
                options.Password.RequiredLength = userConfiguration.PasswordMinLength;
                options.Password.RequiredUniqueChars = userConfiguration.PasswordRequiredUniqueChars;
                options.Lockout.AllowedForNewUsers = userConfiguration.LockoutAllowedForNewUsers;
                options.Lockout.MaxFailedAccessAttempts = userConfiguration.LockoutMaxFailedAccessAttempts;
                options.Lockout.DefaultLockoutTimeSpan = CommonHelper.GetTimeSpanByPeriod(userConfiguration.LockoutType, userConfiguration.LockoutDefaultLockoutTimeSpan);
                options.User.AllowedUserNameCharacters = userConfiguration.UserAllowedUserNameCharacters;
                options.User.RequireUniqueEmail = userConfiguration.UserRequireUniqueEmail;

            }).AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();
            //services.Configure<SecurityStampValidatorOptions>(options =>
            //{
            //    // enables immediate logout, after updating the user's stat.
            //    options.ValidationInterval = TimeSpan.Zero;
            //});
        }

        #endregion
        #region Hangfire Config

        public static void AddHangFireConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x =>
                x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    JobExpirationCheckInterval = TimeSpan.FromDays(1),
                    SlidingInvisibilityTimeout = TimeSpan.FromDays(1),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();
        }
        #endregion
        #region  Jwt Configuration

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SecreteKey);
                var encryptionKey = Encoding.UTF8.GetBytes(jwtConfiguration.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtConfiguration.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtConfiguration.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null && context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            throw new AppException(CustomStatusCodes.UnAuthorized, "Token Expired", HttpStatusCode.Unauthorized, context.Exception, null);
                        }

                        if (context.Exception != null)
                            throw new AppException(CustomStatusCodes.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<AppUser>>();
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity?.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no security stamp");

                        //Find user and token from database and perform your custom validation
                        var userId = claimsIdentity.GetUserId<int>();
                        var user = await userService.GetUserById(userId);

                        if (!user.SecurityStamp.Equals(securityStamp))
                            context.Fail("Token security stamp is not valid.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token security stamp is not valid.");

                        if (user.LockoutEnabled)
                            context.Fail("User is Lockout");

                    },
                    OnChallenge = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                        if (context.AuthenticateFailure != null)
                        {
                            context.Response.Headers.Add("Require-Login", "true");

                            throw new AppException(CustomStatusCodes.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);

                        }
                        context.Response.Headers.Add("No-Access", "true");
                        // throw new AppException(CustomStatusCodes.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                        throw new UnauthorizedAccessException("require login");
                        //return Task.CompletedTask;
                    }
                };
            });
        }

        #endregion

        #region Add Custom Cors

        public static void AddCustomCors(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                var corsConfiguration = configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
                options.AddPolicy("BlogPolicy",
                    builder =>
                    {
                        if (corsConfiguration.AllowAllOrigins)
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            var allCors = corsConfiguration.Origins.Split(",");
                            builder.WithOrigins(allCors);
                        }

                        if (corsConfiguration.AllowAllHeaders)
                        {
                            builder.AllowAnyMethod();
                        }
                        else
                        {
                            var allHeaders = corsConfiguration.Headers.Split(",");
                            builder.WithHeaders(allHeaders);
                        }

                        if (corsConfiguration.AllowAllMethods)
                        {
                            builder.AllowAnyMethod();
                        }
                        else
                        {
                            var allMethods = corsConfiguration.Methods.Split(",");
                            builder.WithMethods(allMethods);
                        }

                    });
            });
        }
        #endregion
    }
}
