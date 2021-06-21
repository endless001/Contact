using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.API.Models
{
    [BsonIgnoreExtraElements]
    public class ContactRequest
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public int Sex { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public int RequestAccountId { get; set; }
        public string RequestStatus { get; set; }
        public DateTime HandleTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
