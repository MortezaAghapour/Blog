using Blog.Application.Dtos.Users;

namespace Blog.Infrastructure.Contracts.Helpers
{
    public interface IWorkContext
    {
        UserDto CurrentUser { get; }
    }
}