using MyChat.Domain.Common;
using MyChat.Domain.Events;
using System.Net;
using System.Net.Mail;

namespace MyChat.Domain.Handlers
{
    public class UserCreatedEventHandler : IHandle<UserCreatedEvent>
    {
        private readonly IEmailOptions _emailOptions;

        public UserCreatedEventHandler(IEmailOptions emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public void Handle(UserCreatedEvent domainEvent)
        {
            // Send Welcome Mail To User
            //SmtpClient client = new SmtpClient(_emailOptions.SmptServer);
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password);

            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(_emailOptions.EmailAddress);
            //mailMessage.To.Add(domainEvent.User.EmailAddress);
            //mailMessage.Body = "body";
            //mailMessage.Subject = "subject";
            //client.Send(mailMessage);
        }
    }
}
