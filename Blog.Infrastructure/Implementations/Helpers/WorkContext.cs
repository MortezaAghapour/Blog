using Blog.Application.Dtos.Users;
using Blog.Infrastructure.Contracts.Helpers;
using Blog.Infrastructure.Identity.Services.Users;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Implementations.Helpers
{
    public class WorkContext:IWorkContext,IScopedLifeTime
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        #endregion
        #region Constructors
        public WorkContext(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        #endregion
        #region Methods

        #endregion

        public UserDto CurrentUser
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User is null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                var loggedUser = _userService.GetUserByHttpContextUser(_httpContextAccessor.HttpContext.User).GetAwaiter().GetResult();
                return loggedUser.Adapt<UserDto>();
            }
        }
    }
}