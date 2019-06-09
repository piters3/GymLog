using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Enabled { get; set; }
        public Gender Gender { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Height { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
