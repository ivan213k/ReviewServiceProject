using Microsoft.AspNetCore.Identity;

namespace ReviewService.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
