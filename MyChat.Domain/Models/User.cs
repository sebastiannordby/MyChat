using MyChat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Domain.Models
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string EmailAddress { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
       
        public User(
            Guid id,
            string userName,
            string password,
            string emailAddress,
            string firstName,
            string lastName) : base(id)
        {
            this.UserName = userName;
            this.Password = password;
            this.EmailAddress = emailAddress;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
