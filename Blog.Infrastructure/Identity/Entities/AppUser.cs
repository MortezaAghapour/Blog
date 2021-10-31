using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Entities
{
    public class AppUser   :IdentityUser<long>
    {
        #region Fields

        public string FirstName { get; set; }
        public string LastName { get; set; }

        #endregion
    }
}