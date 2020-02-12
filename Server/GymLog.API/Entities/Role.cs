using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; private set; }
    }
}
