using Contact.API.Data;
using Contact.API.Infrastructure.Services;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Contact.API.Grpc
{
    public class ContactGrpcService:ContactGrpc.ContactGrpcBase
    {
        private readonly IAccountService _accountService;
        private readonly IIdentityService _identityService;
        private readonly IContactBookRepository _contactBookRepository;
        private readonly IContactRequestRepository _contactRequestRepository;
        private readonly IGroupRepository _groupRepository;
        public ContactGrpcService(IAccountService accountService,
            IIdentityService identityService,
            IContactBookRepository contactBookRepository,
            IContactRequestRepository contactRequestRepository,
            IGroupRepository groupRepository
            )
        {
            _accountService = accountService;
            _identityService = identityService;
            _contactBookRepository = contactBookRepository;
            _contactRequestRepository = contactRequestRepository;
            _groupRepository = groupRepository;

        }
        
        public override Task<ContactBookResponse> AddContact(ContactRequest request, ServerCallContext context)
        {
            return base.AddContact(request, context);
        }

        public override Task<ContactBookResponse> GetContactList(ContactRequest request, ServerCallContext context)
        {
            return base.GetContactList(request, context);
        }

        public override Task<ContactBookResponse> UpdateContact(ContactRequest request, ServerCallContext context)
        {
            return base.UpdateContact(request, context);
        }
    }
}
