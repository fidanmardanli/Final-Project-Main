using Microsoft.AspNetCore.Identity;

namespace AASA_Back_End.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }

        public bool IsActive { get; set; } = false;
    }
}
