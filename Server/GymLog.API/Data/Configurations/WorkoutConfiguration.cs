using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class WorkoutConfiguration : AuditableEntityConfiguration<Workout>
    {
        public override void Configure(EntityTypeBuilder<Workout> builder)
        {
            base.Configure(builder);

            builder.ToTable("Workouts");
        }
    }
}
