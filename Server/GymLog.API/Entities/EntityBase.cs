using System;

namespace GymLog.API.Entities
{
    public abstract class EntityBase : IIdentifiable
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }

        protected EntityBase()
        {
            CreatedDate = DateTime.UtcNow;
            SetUpdatedDate();
        }

        protected virtual void SetUpdatedDate() => UpdatedDate = DateTime.UtcNow;
    }
}
