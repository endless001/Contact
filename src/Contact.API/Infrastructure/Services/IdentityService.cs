using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetIdentity()
        {
            return _context.HttpContext.User.FindFirst("sub").Value;
        }

        public string GetName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}
