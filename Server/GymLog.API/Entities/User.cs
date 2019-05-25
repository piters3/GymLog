﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
