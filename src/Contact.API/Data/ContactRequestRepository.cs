using Contact.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.API.Data
{
    public class ContactRequestRepository : IContactRequestRepository
    {
        private readonly ContactDbContext _context;
        public ContactRequestRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddContactRequestAsync(int accountId, int requestAcocountId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactRequest>.Filter.Eq(a => a.AccountId, accountId) &
              Builders<ContactRequest>.Filter.Eq(a => a.RequestAccountId, requestAcocountId);

            var contactRequest = (await _context.ContactRequests.FindAsync(filter)).FirstOrDefault(cancellationToken);
            if (contactRequest != null)
            {
                var result = await _context.ContactRequests.UpdateOneAsync(filter, Builders<ContactRequest>.Update.Set("CreateTime", DateTime.Now));
                return result.MatchedCount == result.ModifiedCount;
            }

            await _context.ContactRequests.InsertOneAsync(new ContactRequest()
            {
                RequestAccountId = accountId,
                AccountId = requestAcocountId

            }, null, cancellationToken);

            return true;

        }

        public async Task<List<ContactRequest>> GetContactRequestListAsync(int accountId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactRequest>.Filter.Eq(a => a.AccountId, accountId);
            return (await _context.ContactRequests.FindAsync(filter)).ToList();
        }

        public async Task<bool> HandleContactRequestAsync(int accountId, int requestAcocountId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactRequest>.Filter.Eq(a => a.AccountId, accountId) &
                Builders<ContactRequest>.Filter.Eq(a => a.RequestAccountId, requestAcocountId);
            var contactRequest = (await _context.ContactRequests.FindAsync(filter)).FirstOrDefault();
            if (contactRequest != null)
            {
                await _context.ContactRequests.UpdateOneAsync(filter, Builders<ContactRequest>.Update.Set("HandleTime", DateTime.Now)
                    .Set("RequestStatus", 1), null, cancellationToken);
            }
            return true;
        }
    }
}
