namespace Blog.Application.Dtos.Users
{
    public class AuditUserInfoDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}