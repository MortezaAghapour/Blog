using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Infrastructure.Identity.Entities;
using Blog.Shared.Exceptions;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Identity.Services.Users
{
    public class UserService : IUserService,IScopedLifeTime
    {
        #region Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        #endregion
        #region Constructors
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        #endregion
        #region Methods
        public async Task<AppUser> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new NullArgumentException($"The Fields {nameof(userName)} Empty in   {GetType().Name} / {MethodBase.GetCurrentMethod().Name} ");
            }

            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<SignInResult> CheckPassword(AppUser user, string password)
        {
            if (user is null)
            {
                throw new NullArgumentException($"The Fields {nameof(user)} Empty in   {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new NullArgumentException($"فیلدThe Fields {nameof(password)} Empty in   {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            return await _signInManager.CheckPasswordSignInAsync(user, password, true);

        }

        public async Task<AppUser> GetUserById(long id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IdentityResult> AddUser(AppUser appUser, string password)
        {
            if (appUser is null)
            {
                throw new NullArgumentException($"The Fields {nameof(appUser)} Empty in   {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");

            }

            if (string.IsNullOrEmpty(password))
            {
                throw new NullArgumentException($"The Fields {nameof(password)} Empty in   {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");

            }

            var result = await _userManager.CreateAsync(appUser, password);
            return result;
        }

        public async Task UpdateLockoutToZero(AppUser user)
        {
            await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.UtcNow));
        }

        public async Task<AppUser> GetUserByHttpContextUser(ClaimsPrincipal httpContextUser)
        {
            return await _userManager.GetUserAsync(httpContextUser);
        }

        #endregion
    }
}