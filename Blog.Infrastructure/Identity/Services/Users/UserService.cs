using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Application.Dtos.AppSettings.Jwt;
using Blog.Application.Dtos.Commons;
using Blog.Infrastructure.Identity.Dtos;
using Blog.Infrastructure.Identity.Entities;
using Blog.Infrastructure.Jwt;
using Blog.Shared.Exceptions;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Blog.Shared.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Identity.Services.Users
{
    public class UserService : IUserService,IScopedLifeTime
    {
        #region Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;

        #endregion
        #region Constructors
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }
        #endregion
        #region Methods

        public async Task<ReturnDto<JwtDto>> Login(LoginDto model)
        {
            var result = new ReturnDto<JwtDto>
            {
                IsSuccess = false
            };
            var user = await GetUserByUserName(model.UserName);
            if (user is null)
            {
                result.Errors.Add(ValidationErrorResources.UserNotFoundWithEnteredInfo);
                return result;
            }

            var checkPassword = await CheckPassword(user, model.Password);
            if (checkPassword.IsLockedOut)
            {
                result.Errors.Add(ValidationErrorResources.UserIsLockOut);
                return result;
            }

            if (checkPassword.IsNotAllowed)
            {
                result.Errors.Add(ValidationErrorResources.UserNotAllowed);
                return result;
            }

            await UpdateLockoutToZero(user);

            result.IsSuccess = true;
            var jwt = await _jwtService.Generate(user);
            result.Data = jwt;
            return result;
        }

        public async Task<ReturnDto<EmptyDto>> Register(RegisterDto model)
        {
            var result = new ReturnDto<EmptyDto>
            {
                IsSuccess = false
            };
            var checkUserName = await GetUserByUserName(model.UserName);
            if (!(checkUserName is null))
            {
                result.Errors.Add("کاربری با نام کاربری وارد شده در سیستم موجود می باشد");
                return result;
            }

            var appUser = new AppUser
            {
                FirstName = model.Name,
                LastName = model.LastName,
                UserName = model.UserName
            };
            var register = await AddUser(appUser, model.Password);
            if (!register.Succeeded)
            {
                if (register.Errors != null) result.Errors.AddRange(register.Errors?.Select(c => c.Description));
                return result;
            }

            result.IsSuccess = true;
            result.Message = "کاربر مورد نظر با موفقیت ثبت شد";
            return result;
        }

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