using MyChat.Domain.Common;
using MyChat.Domain.Common.UnitOfWork;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyChat.Infrastructure.Common
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IContainer _container;

        public DomainEventDispatcher(IContainer container)
        {
            _container = container;
        }

        public void DispatchEvent(IDomainEvent domainEvent)
        {
            var handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
            var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = _container.GetAllInstances(handlerType);
            var wrappedHandlers = handlers
                .Cast<object>()
                .Select(handler => (DomainEventHandler) Activator.CreateInstance(wrapperType, handler));

            foreach(var handler in wrappedHandlers)
            {
                handler.Handle(domainEvent);
            }
        }

        private abstract class DomainEventHandler
        {
            public abstract void Handle(IDomainEvent domainEvent);
        }

        private class DomainEventHandler<T> : DomainEventHandler
            where T : IDomainEvent
        {
            private readonly IHandle<T> _handler;

            public DomainEventHandler(IHandle<T> handler)
            {
                _handler = handler;
            }

            public override void Handle(IDomainEvent domainEvent)
            {
                _handler.Handle((T)domainEvent);
            }
        }
    }


}
