using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class ExerciseConfiguration : BaseEntityConfiguration<Exercise>
    {
        public override void Configure(EntityTypeBuilder<Exercise> builder)
        {
            base.Configure(builder);

            builder.ToTable("Exercises")
                .Property(p => p.Name)
                .IsRequired();

            builder.HasMany(x => x.Workouts);
        }
    }
}
