using Microsoft.EntityFrameworkCore;
using System;

namespace GymLog.API.Entities
{
    public abstract class EntityBase : IIdentifiable
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }
        public string UpdatedBy { get; protected set; }

        public virtual void SetAuditProperties(EntityState state, string username)
        {
            var now = DateTime.UtcNow;

            if (state == EntityState.Added)
            {
                CreatedBy = username ?? "unknown";
                CreatedDate = now;
            }

            UpdatedBy = username ?? "unknown";
            UpdatedDate = now;
        }
    }
}
