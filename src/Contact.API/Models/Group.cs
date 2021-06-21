
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.API.Models
{
    [BsonIgnoreExtraElements]
    public class Group
    {
        public string GroupName { get; set; }
        public List<ContactModel> Contacts { get; set; }
    }
}
