using Contact.API.Data;
using Contact.API.Infrastructure.Services;
using Contact.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactBookRepository _contactBookRepository;
        private readonly IContactRequestRepository _contactRequestRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IAccountService _accountService;
        public ContactController(IContactBookRepository contactBookRepository,
            IContactRequestRepository contactRequestRepository,
            IGroupRepository groupRepository,
            IAccountService accountService)
        {
            _contactBookRepository = contactBookRepository;
            _contactRequestRepository = contactRequestRepository;
            _groupRepository = groupRepository;
            _accountService = accountService;
        }

        [Route("addcontact/{requestId}")]
        [HttpPost]
        public async Task<IActionResult> AddContact(int requestId, CancellationToken cancellationToken)
        {
            var accountId = 1;
            var request = await _accountService.GetAccountInfo(requestId);
            var current = await _accountService.GetAccountInfo(accountId);
            await _contactBookRepository.AddContactAsync(requestId, current, cancellationToken);
            await _contactBookRepository.AddContactAsync(accountId, request, cancellationToken);
            return Ok();
        }
        [Route("updatecontact")]
        [HttpPost]
        public async Task<IActionResult> UpdateContact(CancellationToken cancellationToken)
        {
            var accountId = 1;

            var account = await _accountService.GetAccountInfo(accountId);
            var result = await _contactBookRepository.UpdateContactAsync(account
                , cancellationToken);
            return Ok();
        }

        [HttpGet("getcontact")]
        public async Task<IActionResult> GetContactList(CancellationToken cancellationToken)
        {
            var accountId = 1;
            var result = await _contactBookRepository.GetContactListAsync(accountId, cancellationToken);
            return Ok(result);
        }
        [Route("getcontactrequest")]
        [HttpGet]
        public async Task<IActionResult> GetContactRequestList(CancellationToken cancellationToken)
        {
            var accountId = 1;
            var result = await _contactRequestRepository.GetContactRequestListAsync(accountId, cancellationToken);
            return Ok(result);
        }
        [Route("addcontactrequest/{requestId}")]
        [HttpPost]
        public async Task<IActionResult> AddContactRequest(int requestId, CancellationToken cancellationToken)
        {
            var accountId = 1;
            await _contactRequestRepository.AddContactRequestAsync(accountId, requestId, cancellationToken);
            return Ok();
        }
        [Route("handlecontactrequest/{requestId}")]
        [HttpPost]
        public async Task<IActionResult> HandleContactRequest(int requestId, CancellationToken cancellationToken)
        {
            var accountId = 1;
            await _contactRequestRepository.HandleContactRequestAsync(accountId, requestId, cancellationToken);
            return Ok();
        }

        [HttpGet("getgroup")]
        public async Task<IActionResult> GetGroupList(CancellationToken cancellationToken)
        {
            var accountId = 2;
            var result = await _groupRepository.GetGroupListAsync(accountId, cancellationToken);
            return Ok(result);
        }
        [HttpPost("creategroup")]
        public async Task<IActionResult> CreateGroup(string groupName,CancellationToken cancellationToken)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            contacts.Add(new ContactModel()
            {
                AccountId=2
            });
            await _groupRepository.CreateGroupAsync(groupName,contacts, cancellationToken);
            return Ok();
        }
    }
}
