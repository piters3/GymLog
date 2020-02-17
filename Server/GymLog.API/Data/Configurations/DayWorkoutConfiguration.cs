using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class DayWorkoutConfiguration : AuditableEntityConfiguration<DayWorkout>
    {
        public override void Configure(EntityTypeBuilder<DayWorkout> builder)
        {
            base.Configure(builder);
        }
    }
}
