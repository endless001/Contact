using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Models;


namespace Contact.API.Data
{
    public interface IContactRequestRepository
    {
        Task<List<ContactRequest>> GetContactRequestListAsync(int accountId, CancellationToken cancellationToken);
        Task<bool> AddContactRequestAsync(int accountId, int requestAccountId, CancellationToken cancellationToken);
        Task<bool> HandleContactRequestAsync(int accountId, int requestAccountId, CancellationToken cancellationToken);
    }
}
