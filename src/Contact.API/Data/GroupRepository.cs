using Contact.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.API.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ContactDbContext _context;
        public GroupRepository(ContactDbContext context)
        {
            _context = context;
        }

        public  async Task<bool> CreateGroupAsync(string groupName, List<ContactModel> contacts, CancellationToken cancellationToken)
        {
            await _context.Groups.InsertOneAsync(new Group()
            {
                GroupName = groupName,
                Contacts = contacts

            }, null, cancellationToken);

            return true;
        }

        public async Task<List<Group>> GetGroupListAsync(int accountId, CancellationToken cancellationToken)
        {
            var filter = Builders<Group>.Filter.And(Builders<Group>.Filter.ElemMatch(c => c.Contacts, contact => contact.AccountId == accountId));
            return (await _context.Groups.FindAsync(filter)).ToList();
        }
    }
}
