using MyChat.Domain.Common;

namespace MyChat.Infrastructure.Common
{
    public class EmailOptions : IEmailOptions
    {
        public string SmptServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}
