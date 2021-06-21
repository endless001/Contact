using Contact.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountInfo(int id);
    }
}
