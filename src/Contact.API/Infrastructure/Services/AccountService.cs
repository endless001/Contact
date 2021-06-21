using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public Task<AccountModel> GetAccountInfo(int id)
        {
            var account = new AccountModel()
            {
                AccountId = 2,
                AccountName = "lq",

            };
            return Task.FromResult(account);            
        }
    }
}
