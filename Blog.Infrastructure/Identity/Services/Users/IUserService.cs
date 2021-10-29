using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Application.Dtos.AppSettings.Jwt;
using Blog.Application.Dtos.Commons;
using Blog.Infrastructure.Identity.Dtos;
using Blog.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Services.Users
{
    public interface IUserService
    {
        Task<ReturnDto<JwtDto>> Login(LoginDto model);
        Task<ReturnDto<EmptyDto>> Register(RegisterDto model);
        Task<AppUser> GetUserByUserName(string userName);
        Task<SignInResult> CheckPassword(AppUser user, string password);
        Task<AppUser> GetUserById(long id);
        Task<IdentityResult> AddUser(AppUser appUser, string password);
        Task UpdateLockoutToZero(AppUser user);
        Task<AppUser> GetUserByHttpContextUser(ClaimsPrincipal httpContextUser);
    }
}