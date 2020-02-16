using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class WorkoutDaylogConfiguration : IEntityTypeConfiguration<WorkoutDaylog>
    {
        public void Configure(EntityTypeBuilder<WorkoutDaylog> builder)
        {
            builder.ToTable("WorkoutDaylogs");

            builder.HasKey(x => new { x.WorkoutId, x.DaylogId });

            builder.HasOne(x => x.Workout)
                .WithMany(w => w.WorkoutDaylogs)
                .HasForeignKey(x => x.WorkoutId)
                .IsRequired();

            builder.HasOne(x => x.Daylog)
                .WithMany(d => d.WorkoutDaylogs)
                .HasForeignKey(x => x.DaylogId)
                .IsRequired();
        }
    }
}
