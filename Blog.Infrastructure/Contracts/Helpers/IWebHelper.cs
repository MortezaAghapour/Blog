namespace Blog.Infrastructure.Contracts.Helpers
{
    public interface IWebHelper
    {
        string GetUserAgent();
        string GetIpAddress();
    }
}