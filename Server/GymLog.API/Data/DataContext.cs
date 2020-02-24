using GymLog.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
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
        public DbSet<DayWorkout> DayWorkouts { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Daylog> Daylogs { get; set; }
        public DbSet<Routine> Routines { get; set; }

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
            var currentUserId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditableEntity auditableEntity)
                    auditableEntity.SetAuditProperties(entry.State, username);

                if (entry.Entity is IUserId entityWithUserId)
                    entityWithUserId.UserId = currentUserId;
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
                user.Property(p => p.UserName).IsRequired();
                user.Property(p => p.Name).HasMaxLength(100);
                user.Property(p => p.Surname).HasMaxLength(100);
                user.Property(x => x.Weight);
                user.Property(x => x.Height);
                user.Property(x => x.Gender).HasConversion<int>();
                user.HasMany(x => x.Daylogs);
            });

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            builder.Entity<Muscle>(x =>
            {
                x.ToTable("Muscles");
                x.HasKey(x => x.Id);
                x.Property(p => p.Name).HasMaxLength(100).IsRequired();
                x.HasMany(x => x.Exercises);
            });

            builder.Entity<Equipment>(x =>
            {
                x.ToTable("Equipments");
                x.HasKey(x => x.Id);
                x.Property(p => p.Name).HasMaxLength(100).IsRequired();
                x.HasMany(x => x.Exercises);
            });

            builder.Entity<Exercise>(x =>
            {
                x.ToTable("Exercises");
                x.HasKey(x => x.Id);
                x.Property(p => p.Name).HasMaxLength(100).IsRequired();
                x.Property(p => p.DetailedMuscle).HasMaxLength(100);
                x.Property(p => p.OtherMuscles).HasMaxLength(100);
                x.Property(p => p.Type).HasConversion<int>();
                x.Property(p => p.Difficulty).HasConversion<int>();
                x.HasMany(x => x.Workouts);
            });

            builder.Entity<Workout>(x =>
            {
                x.ToTable("Workouts");
                x.HasKey(x => x.Id);
                x.Property(p => p.CreatedBy).HasMaxLength(100);
                x.Property(p => p.UpdatedBy).HasMaxLength(100);
                x.Property(p => p.Version).IsConcurrencyToken().IsRowVersion();
            });

            builder.Entity<Set>(x =>
            {
                x.ToTable("Sets");
                x.HasKey(x => x.Id);
                x.Property(p => p.CreatedBy).HasMaxLength(100);
                x.Property(p => p.UpdatedBy).HasMaxLength(100);
                x.Property(p => p.Version).IsConcurrencyToken().IsRowVersion();
            });

            builder.Entity<DayWorkout>(x =>
            {
                x.ToTable("DayWorkouts");
                x.HasKey(x => x.Id);
                x.Property(p => p.Name).HasMaxLength(100);
                x.Property(p => p.CreatedBy).HasMaxLength(100);
                x.Property(p => p.UpdatedBy).HasMaxLength(100);
                x.Property(p => p.Version).IsConcurrencyToken().IsRowVersion();
            });

            builder.Entity<Daylog>(x =>
            {
                x.ToTable("Daylogs");
                x.HasKey(x => x.Id);
                x.HasMany(x => x.Workouts);
                x.Property(p => p.CreatedBy).HasMaxLength(100);
                x.Property(p => p.UpdatedBy).HasMaxLength(100);
                x.Property(p => p.Version).IsConcurrencyToken().IsRowVersion();
            });

            builder.Entity<Routine>(x =>
            {
                x.ToTable("Routines");
                x.HasKey(x => x.Id);
                x.Property(p => p.Name).HasMaxLength(100).IsRequired();
                x.HasMany(x => x.DayWorkouts);
                x.Property(p => p.CreatedBy).HasMaxLength(100);
                x.Property(p => p.UpdatedBy).HasMaxLength(100);
                x.Property(p => p.Version).IsConcurrencyToken().IsRowVersion();
            });

            //builder.ApplyConfiguration(new MuscleConfiguration());
            //builder.ApplyConfiguration(new EquipmentConfiguration());
            //builder.ApplyConfiguration(new ExerciseConfiguration());
            //builder.ApplyConfiguration(new WorkoutConfiguration());
            //builder.ApplyConfiguration(new RoutineConfiguration());
            //builder.ApplyConfiguration(new DayWorkoutConfiguration());
            //builder.ApplyConfiguration(new DaylogConfiguration());
        }
    }
}
