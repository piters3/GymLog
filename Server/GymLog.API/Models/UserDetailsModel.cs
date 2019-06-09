using GymLog.API.Entities;
using System;
using System.Collections.Generic;

namespace GymLog.API.Models
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<RoleModel> Roles { get; set; }
    }
}
