using GymLog.API.Data.Configurations;
using GymLog.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GymLog.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Daylog> Daylogs { get; set; }
        public DbSet<WorkoutDaylog> WorkoutDaylogs { get; set; }

        public override int SaveChanges()
        {
            SetAuditProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditProperties()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is null)
                return;

            var username = httpContext.User.Identity.Name;
            //var authenticatedUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditableEntity;
                entity?.SetAuditProperties(entry.State, username);
            }
        }

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
                user.Property(x => x.Weight);
                user.Property(x => x.Height);
                user.Property(x => x.Gender).HasConversion<int>();
                user.HasMany(x => x.Workouts);
                user.HasMany(x => x.Daylogs);
            });

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            builder.ApplyConfiguration(new MuscleConfiguration());
            builder.ApplyConfiguration(new EquipmentConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new WorkoutConfiguration());
            builder.ApplyConfiguration(new DaylogConfiguration());
            builder.ApplyConfiguration(new WorkoutDaylogConfiguration());
        }
    }
}
