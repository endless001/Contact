using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Models;

namespace Contact.API.Data
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetGroupListAsync(int accountId, CancellationToken cancellationToken);

        Task<bool> CreateGroupAsync(string groupName, List<ContactModel> contacts, CancellationToken cancellationToken);

    }
}
