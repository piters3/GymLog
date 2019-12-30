using System;
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
        private readonly DataContext _ctx;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext ctx)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _ctx = ctx;
        }

        public void Run()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            SeedUsers();
            SeetGymData();
        }

        private void SeedUsers()
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

        private void SeetGymData()
        {
            var muscles = new List<Muscle>
            {
                new Muscle("Triceps"),
                new Muscle("Klatka piersiowa"),
                new Muscle("Barki"),
                new Muscle("Biceps"),
                new Muscle("Brzuch"),
                new Muscle("Plecy"),
                new Muscle("Przedramię"),
                new Muscle("Uda"),
                new Muscle("Pośladki"),
                new Muscle("Łydki"),
                new Muscle("Cardio")
            };

            muscles.ForEach(x => _ctx.Muscles.Add(x));

            var equipments = new List<Equipment>
            {
                new Equipment("Hantel"),
                new Equipment("Ławka prosta"),
                new Equipment("Wyciąg"),
                new Equipment("Suwnica"),
                new Equipment("Sztanga prosta"),
                new Equipment("Sztanga gięta"),
                new Equipment("Drążek")
            };

            equipments.ForEach(x => _ctx.Equipments.Add(x));

            var exercises = new List<Exercise>
            {
                new Exercise("Wyciskanie sztangi leżąc", "Bla bla bla",  muscles[1], equipments[1] ),
                new Exercise("Przysiady", "Bla bla bla", muscles[7], equipments[3]),
                new Exercise("Podciąganie", "Bla bla bla", muscles[5], equipments[6])
            };
            exercises.ForEach(x => _ctx.Exercises.Add(x));

            var user = _ctx.Users.First();

            var workouts = new List<Workout>
            {
                new Workout(5, 5, 70, user, exercises[0]),
                new Workout(5, 4, 85, user, exercises[1]),
                new Workout(3, 14, 90, user, exercises[0]),
                new Workout(5, 5, 5, user, exercises[2]),
                new Workout(5, 18, 90, user, exercises[0])
            };

            workouts.ForEach(x => _ctx.Workouts.Add(x));

            var daylogs = new List<Daylog>
            {
                new Daylog(DateTime.Today, user),
                new Daylog(DateTime.Today.AddDays(-2), user)
            };

            daylogs.ForEach(x => _ctx.Daylogs.Add(x));

            var wds = new List<WorkoutDaylog>
            {
                new WorkoutDaylog(workouts[0], daylogs[0]),
                new WorkoutDaylog(workouts[1], daylogs[0]),
                new WorkoutDaylog(workouts[2], daylogs[0]),
                new WorkoutDaylog(workouts[3], daylogs[0]),
                new WorkoutDaylog(workouts[4], daylogs[0]),
                new WorkoutDaylog(workouts[0], daylogs[1]),
                new WorkoutDaylog(workouts[1], daylogs[1]),
                new WorkoutDaylog(workouts[4], daylogs[1])
            };

            wds.ForEach(x => _ctx.WorkoutDaylogs.Add(x));

            _ctx.SaveChanges();
        }
    }
}
