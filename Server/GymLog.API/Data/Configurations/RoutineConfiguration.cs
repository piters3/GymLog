using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class RoutineConfiguration : AuditableEntityConfiguration<Routine>
    {
        public override void Configure(EntityTypeBuilder<Routine> builder)
        {
            base.Configure(builder);

            builder.ToTable("Routines")
                .Property(p => p.Name)
                .IsRequired();
        }
    }
}
