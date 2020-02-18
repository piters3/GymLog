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
                new Muscle("Abs"),
                new Muscle("Back"),
                new Muscle("Biceps"),
                new Muscle("Chest"),
                new Muscle("Forearm"),
                new Muscle("Glutes"),
                new Muscle("Shoulders"),
                new Muscle("Triceps"),
                new Muscle("Upper legs"),
                new Muscle("Lower legs"),
                new Muscle("Cardio")
            };

            muscles.ForEach(x => _ctx.Muscles.Add(x));

            var equipments = new List<Equipment>
            {
                new Equipment("Bands"),
                new Equipment("Barbell"),
                new Equipment("Bench"),
                new Equipment("Body only"),
                new Equipment("Dumbbell"),
                new Equipment("Exercise Ball"),
                new Equipment("EZ - Bar"),
                new Equipment("Kettlebell"),
                new Equipment("Machine - Cardio"),
                new Equipment("Machine - Strength"),
                new Equipment("Other"),
                new Equipment("Pull Bar"),
                new Equipment("Weight Plate"),
            };

            equipments.ForEach(x => _ctx.Equipments.Add(x));

            var exercises = new List<Exercise>
            {
                new Exercise("Barbell Clean Deadlift",
                @"Steps:
                1. To begin this exercise; start off with a weighted barbell right in front of your shins with an overhand grip.
                2. Bend down at the knees with your back straight, chest out, head facing forward and pull up on the barbell with the driving force from your heels.
                3. As you lift the barbell up, keep your back straight and pull up.
                4. When the barbell crosses your knees, lift up with your torso until the bar has reached your hips and your knees are fully extended.
                5. Repeat this exercise for as many repetitions as needed",
                ExerciseType.Olympic, Difficulty.Intermediate, muscles[8], equipments[1], "Hamstrings", "Back, Glutes"),
                new Exercise("Barbell Bench Press",
                @"Steps:
                1. Lie with your back flat on a bench with your feet firmly on the ground and the bar resting on the bench’s rack.
                2. Slowly lift the bar off the rack and hold it above your chest as this will be the starting position.
                3. Then lower the bar down until it is slightly above your chest, making sure that it doesn't touch or slam against your chest.
                4. Hold this position briefly and make sure you have complete control of the bar, then raise it back up to the starting postion.
                3. Place the bar on the rack and that will complete your set.",
                ExerciseType.Strength, Difficulty.Beginner, muscles[3], equipments[1], "Full",  "Triceps, Shoulders"),
                new Exercise("Hammer Grip Pull Up",
                @"Steps :
                1. Start off standing in front of a assisted pull up machine and grab the hammer grip section of the bar.
                2. Lift your feet up off of the floor and pull up slowly, squeezing tightly on your lats until your shoulders are at level with your head.
                3. Hold this position for a count then return back to the starting position.
                4. Repeat for as many reps and sets as desired.",
                ExerciseType.Strength, Difficulty.Intermediate, muscles[1], equipments[11], "Lats", "Shoulders"),
                new Exercise("Dumbbell Lateral Raise",
                @"Steps :
                1. Start off standing up straight with your feet shoulder-width apart, keeping your abs tight and holding a dumbbell in each hand with your palms facing towards your body.
                2. Slightly bend at your knees and then slowly raise your arms at your sides until your palms face the floor.
                3. Once you reach the top position, hold for a count, squeezing your shoulder muscles then return back to the starting position.",
                ExerciseType.Strength, Difficulty.Beginner, muscles[6], equipments[4], "Traps"),
                new Exercise("Dumbbell Incline Two Arm Extension",
                @"Steps:
                1. Start off sitting your back flat on an incline bench with your feet on the floor in front of you, holding a dumbbell on your legs.
                2. Elevate the weight above your head as this will be your starting position.
                3. Slowly lower the dumbbell behind your head as far as possible by extending both arms and squeeze your triceps.
                4. Hold for a count then return back to the starting position.
                5. Repeat for as many reps and sets as desired.",
                ExerciseType.Strength, Difficulty.Beginner, muscles[7], equipments[4]),
                new Exercise("Barbell Bicep Drag Curl",
                @"Steps :
                1. To begin this exercise; take a barbell with palms facing forward with the barbell resting at your pelvis.
                2. Then take the barbell and curl it towards your upper chest as in a way to “Drag” the bar up.
                3. Hold and squeeze the biceps tightly.
                4. Return the barbell back down to the starting position keeping the barbell in contact with your body.
                5. Repeat this exercise for as many repetitions as needed.",
                ExerciseType.Strength, Difficulty.Intermediate, muscles[2], equipments[6], null, "Forearm")
            };

            exercises.ForEach(x => _ctx.Exercises.Add(x));

            var workouts = new List<Workout>
            {
                new Workout(exercises[0], new List<Set> { new Set(1, 3, 100), new Set(2, 3, 100), new Set(3, 4, 105), new Set(4, 4, 105), new Set(5, 4, 110) }),
                new Workout(exercises[1], new List<Set> { new Set(1, 5, 85), new Set(2, 5, 85), new Set(3, 4, 87.5f), new Set(4, 4, 87.5f), new Set(5, 5, 87.5f) }),
                new Workout(exercises[2], new List<Set> { new Set(1, 5, 7), new Set(2, 5, 7), new Set(3, 5, 7), new Set(4, 5, 7) }),
                new Workout(exercises[3], new List<Set> { new Set(1, 10, 9), new Set(2, 10, 9), new Set(3, 10, 9), new Set(4, 10, 9) }),
                new Workout(exercises[4], new List<Set> { new Set(1, 10, 27), new Set(2, 10, 27), new Set(3, 10, 30), new Set(4, 10, 30) }),
                new Workout(exercises[5], new List<Set> { new Set(1, 8, 30), new Set(2, 8, 30), new Set(3, 9, 30), new Set(4, 10, 30) })
            };

            workouts.ForEach(x => _ctx.Workouts.Add(x));

            var user = _ctx.Users.First();
            var daylog = new Daylog(DateTime.Today.AddDays(-2), workouts, user);

            _ctx.Daylogs.Add(daylog);

            //var dayWorkouts = new List<DayWorkout>
            //{
            //    new DayWorkout("Lower Body Workout Day", Day.Monday, workouts),
            //    new DayWorkout("Upper Body Workout Day", Day.Wednesday, workouts),
            //    new DayWorkout("Abs & Core Workout Day", Day.Friday, workouts)
            //};

            //dayWorkouts.ForEach(x => _ctx.DayWorkouts.Add(x));

            //var routine = new Routine("Sample Beginner Routine", 3, "This routine was designed for bodybuilding beginners who want to gain muscle", true, user, dayWorkouts);

            //_ctx.Routines.Add(routine);

            _ctx.SaveChanges();
        }
    }
}
