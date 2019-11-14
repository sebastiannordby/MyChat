using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Domain.Common
{
    public interface IEmailOptions
    {
        string SmptServer { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string EmailAddress { get; set; }
    }
}
