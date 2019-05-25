using System;
using System.Collections.Generic;

namespace GymLog.API.Models
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<RoleModel> Roles { get; set; }
    }
}
