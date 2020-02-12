using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class UserRole: IdentityUserRole<int>
    {
        public virtual User User { get; private set; }
        public virtual Role Role { get; private set; }
    }
}
