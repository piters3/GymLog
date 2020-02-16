namespace GymLog.API.Entities
{
    public abstract class BaseEntity : IIdentifiable
    {
        public int Id { get; }
    }
}
