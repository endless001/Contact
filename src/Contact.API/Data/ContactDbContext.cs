using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Configuration;
using Contact.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Contact.API.Data
{
    public class ContactDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;

        public ContactDbContext(IOptions<MongoConnection> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);
            if (_client != null)
            {
                _database = _client.GetDatabase(options.Value.Database);
            }
        }

        public IMongoCollection<ContactBook> ContactBooks => _database.GetCollection<ContactBook>("ContactBook");
        public IMongoCollection<ContactRequest> ContactRequests => _database.GetCollection<ContactRequest>("ContactRequest");

        public IMongoCollection<Group> Groups => _database.GetCollection<Group>("Group");
    }
}
