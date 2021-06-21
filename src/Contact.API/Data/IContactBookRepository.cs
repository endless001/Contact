using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Models;

namespace Contact.API.Data
{
    public interface IContactBookRepository
    {
        Task<bool> AddContactAsync(int accountId, AccountModel account, CancellationToken cancellationToken);
        Task<bool> UpdateContactAsync(AccountModel account, CancellationToken cancellationToken);
        Task<List<ContactModel>> GetContactListAsync(int accountId, CancellationToken cancellationToken);
    }
}
