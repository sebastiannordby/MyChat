using MyChat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Infrastructure.Common
{
    public interface IDomainEventDispatcher
    {
        void DispatchEvent(IDomainEvent domainEvent);
    }
}
