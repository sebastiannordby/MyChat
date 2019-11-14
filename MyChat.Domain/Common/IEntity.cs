using System;
using System.Collections.Generic;

namespace MyChat.Domain.Common
{
    public interface IEntity
    {
        Guid Id { get; }
        List<IDomainEvent> DomainEvents { get; }
    }
}
