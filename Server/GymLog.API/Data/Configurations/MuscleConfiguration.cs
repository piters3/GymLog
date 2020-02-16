using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class MuscleConfiguration : BaseEntityConfiguration<Muscle>
    {
        public override void Configure(EntityTypeBuilder<Muscle> builder)
        {
            base.Configure(builder);

            builder.ToTable("Muscles")
                .Property(p => p.Name)
                .IsRequired();
        }
    }
}
