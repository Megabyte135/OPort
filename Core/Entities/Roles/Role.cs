using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Roles
{
    public class Role : IdentityRole
    {
        public string Name { get; set; }
    }
}
