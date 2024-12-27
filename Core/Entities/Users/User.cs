using Core.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
