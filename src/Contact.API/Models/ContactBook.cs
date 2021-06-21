using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.API.Models
{

    [BsonIgnoreExtraElements]
    public class ContactBook
    {
        public int AccountId { get; set; }

        public List<ContactModel> ContactBooks { get; set; }
    }
}
