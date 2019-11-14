using System;

namespace MyChat.Infrastructure.Models
{
    public class ApiKey
    {
        public Guid Id { get; }
        public string Key { get; }
        public string Owner { get; }
        public string IssuedBy { get; }
        public DateTime IssuedDateTime { get; }
        public DateTime ValidFromDateTime { get; }
        public DateTime ValidToDateTime { get; }

        public ApiKey(
            Guid id,
            string key,
            string owner,
            string issuedBy,
            DateTime issuedDateTime,
            DateTime validFromDateTime,
            DateTime validToDateTime)
        {
            this.Id = id;
            this.Key = key;
            this.Owner = owner;
            this.IssuedBy = issuedBy;
            this.IssuedDateTime = issuedDateTime;
            this.ValidFromDateTime = validFromDateTime;
            this.ValidToDateTime = validToDateTime;
        }

        public bool IsValid()
        {
            return ValidFromDateTime <= DateTime.Now && ValidToDateTime > DateTime.Now;            
        }
    }
}
