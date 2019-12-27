using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class UserRole: IdentityUserRole<int>
    {
        public User User { get; private set; }
        public Role Role { get; private set; }
    }
}
