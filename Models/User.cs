using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; } = false;
    }
}
