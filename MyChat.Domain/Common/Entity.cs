using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Domain.Common
{
    public class Entity : IEntity
    {
        public Guid Id { get; private set; }
        public List<IDomainEvent> DomainEvents { get; private set; }

        public Entity(Guid id)
        {
            Id = id;
            DomainEvents = new List<IDomainEvent>();
        }
    }
}
