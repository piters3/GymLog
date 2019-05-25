using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
