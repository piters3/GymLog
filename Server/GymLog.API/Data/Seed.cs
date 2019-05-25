using System.Collections.Generic;
using System.Linq;
using GymLog.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var users = new List<User>
                {
                    new User { UserName = "Piotr"},
                    new User { UserName = "Test"}
                };

                var roles = new List<Role>
                {
                    new Role { Name = "User" },
                    new Role { Name = "Admin" }
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "qweqwe").Wait();
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }

                var adminUser = new User
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "qweqwe").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Admin" }).Wait();
                }
            }
        }
    }
}
