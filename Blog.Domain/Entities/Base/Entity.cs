using Blog.Shared.Markers.Entities;

namespace Blog.Domain.Entities.Base
{
    public abstract class Entity<TKey>:IEntity
    {
        public TKey Id { get; set; }
    }
    public abstract class Entity:Entity<long>
    {

    }
}