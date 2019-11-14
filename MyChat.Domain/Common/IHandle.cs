namespace MyChat.Domain.Common
{
    public interface IHandle<T> where T : IDomainEvent
    {
        public void Handle(T domainEvent);
    }
}
