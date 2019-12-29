using GymLog.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Daylog> Daylogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<User>(user =>
            {
                user.ToTable("Users");
                user.Property(x => x.Weight).HasColumnType("decimal(18, 2)");
                user.Property(x => x.Height).HasColumnType("decimal(18, 2)");
                user.Property(x => x.Gender).HasConversion<int>();
                user.HasMany(x => x.Workouts);
                user.HasMany(x => x.Daylogs);
            });

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            builder.Entity<Muscle>()
                .ToTable("Muscle");

            builder.Entity<Equipment>()
                .ToTable("Equipments")
                .HasMany(x => x.Exercises);

            builder.Entity<Exercise>()
                .ToTable("Exercises")
                .HasMany(x => x.Workouts);

            builder.Entity<Workout>().ToTable("Workouts");

            builder.Entity<Daylog>().ToTable("Daylogs");

            builder.Entity<WorkoutDaylog>(wd =>
            {
                wd.ToTable("WorkoutDaylogs");
                wd.HasKey(x => new { x.WorkoutId, x.DaylogId });

                wd.HasOne(x => x.Workout)
                    .WithMany(d => d.WorkoutDaylogs)
                    .HasForeignKey(x => x.WorkoutId)
                    .IsRequired();

                wd.HasOne(x => x.Daylog)
                    .WithMany(d => d.WorkoutDaylogs)
                    .HasForeignKey(x => x.DaylogId)
                    .IsRequired();
            });
        }
    }
}
