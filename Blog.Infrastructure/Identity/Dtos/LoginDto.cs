using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Blog.Infrastructure.Identity.Dtos
{
    public class LoginDto
    {
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [DisplayName("رمز عبور")]
        public string Password { get; set; }  
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
    }
}
