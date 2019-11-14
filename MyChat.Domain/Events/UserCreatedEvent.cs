using MyChat.Domain.Common;
using MyChat.Domain.Models;

namespace MyChat.Domain.Events
{
    public class UserCreatedEvent : IDomainEvent
    {
        public User User { get; }

        public UserCreatedEvent(User user)
        {
            this.User = user;
        }
    }
}
