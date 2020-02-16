using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class DaylogConfiguration : AuditableEntityConfiguration<Daylog>
    {
        public override void Configure(EntityTypeBuilder<Daylog> builder)
        {
            base.Configure(builder);

            builder.ToTable("Daylogs");
        }
    }
}
