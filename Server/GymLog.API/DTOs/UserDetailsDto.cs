using System;
using System.Collections.Generic;
using GymLog.API.Entities;

namespace GymLog.API.DTOs
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public Gender Gender { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
    }
}
