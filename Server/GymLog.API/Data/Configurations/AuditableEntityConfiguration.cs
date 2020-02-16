using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLog.API.Data.Configurations
{
    public class AuditableEntityConfiguration<T> : BaseEntityConfiguration<T> where T : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Version)
                .IsConcurrencyToken()
                .IsRowVersion();
        }
    }
}
