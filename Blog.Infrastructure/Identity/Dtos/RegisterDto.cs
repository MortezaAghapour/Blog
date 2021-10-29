using System.ComponentModel;

namespace Blog.Infrastructure.Identity.Dtos
{
    public class RegisterDto
    {
      
        [DisplayName("نام")]
        public string Name { get; set; } 
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; } 
        [DisplayName("نام کاربری")]
        public string UserName { get; set; } 
        [DisplayName("رمز عبور")]
        public string Password { get; set; }
    }
}