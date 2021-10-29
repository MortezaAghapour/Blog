using System.Threading.Tasks;
using Blog.Application.Dtos.AppSettings.Jwt;
using Blog.Infrastructure.Identity.Entities;

namespace Blog.Infrastructure.Jwt
{
    public interface IJwtService
    {
        Task<JwtDto> Generate(AppUser user);
    }
}