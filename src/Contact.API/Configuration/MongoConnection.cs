using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Configuration
{
    public class MongoConnection
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
