using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class EquipmentConfiguration : BaseEntityConfiguration<Equipment>
    {
        public override void Configure(EntityTypeBuilder<Equipment> builder)
        {
            base.Configure(builder);

            builder.ToTable("Equipments")
             .Property(p => p.Name)
             .IsRequired();

            builder.HasMany(x => x.Exercises);
        }
    }
}
