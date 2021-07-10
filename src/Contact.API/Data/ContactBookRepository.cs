using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Models;
using MongoDB.Driver;

namespace Contact.API.Data
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly ContactDbContext _context;
        public ContactBookRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddContactAsync(int accountId, AccountModel account, CancellationToken cancellationToken)
        {
            var contactBook = (await _context.ContactBooks.FindAsync(a =>a.AccountId==accountId, cancellationToken: cancellationToken)).FirstOrDefault();
            if (contactBook == null)
            {
                contactBook = new ContactBook()
                {
                    AccountId = accountId,
                    ContactBooks = new List<ContactModel>()
                };
                await _context.ContactBooks.InsertOneAsync(contactBook, null, cancellationToken);
            }
            if (contactBook.ContactBooks.Any(u => u.AccountId == account.AccountId))
            {
                return true;
            }
            var filter = Builders<ContactBook>.Filter.Eq(c => c.AccountId, accountId);
            var update = Builders<ContactBook>.Update.AddToSet(c => c.ContactBooks, new ContactModel()
            {
                AccountId = account.AccountId,
                Tel = account.Tel,
                Sex = account.Sex,
                Avatar = account.Avatar,
                Email = account.Email,
                Phone = account.Phone,
                AccountName = account.AccountName
            });

            var updateResult = await _context.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);
            return updateResult.MatchedCount == updateResult.ModifiedCount;
        }

        public async Task<List<ContactModel>> GetContactListAsync(int accountId, CancellationToken cancellationToken)
        {
            var contactBook = (await _context.ContactBooks.FindAsync(a => a.AccountId == accountId)).FirstOrDefault();
            if (contactBook == null)
            {
                return new List<ContactModel>();
            }
            return contactBook.ContactBooks;
        }

        public async Task<bool> UpdateContactAsync(AccountModel account, CancellationToken cancellationToken)
        {
            var contactBook = (await _context.ContactBooks.FindAsync(c => c.AccountId == account.AccountId, null, cancellationToken)).
                FirstOrDefault(cancellationToken);
            if (contactBook == null)
            {
                return true;
            }
            var contactIds = contactBook.ContactBooks.Select(u => u.AccountId);


            var filter = Builders<ContactBook>.Filter.And(
                    Builders<ContactBook>.Filter.In(c => c.AccountId, contactIds),
                    Builders<ContactBook>.Filter.ElemMatch(c => c.ContactBooks, contact => contact.AccountId == account.AccountId));

            var update = Builders<ContactBook>.Update
                 .Set("UserContacts.$.AccountName", account.AccountName)
                 .Set("UserContacts.$.Avatar", account.Avatar)
                 .Set("UserContacts.$.Phone", account.Phone)
                 .Set("UserContacts.$.Sex", account.Sex)
                 .Set("UserContacts.$.Tel", account.Tel)
                 .Set("UserContacts.$.Email", account.Email);
            var result = _context.ContactBooks.FindAsync(filter);
            var updateResult = await _context.ContactBooks.UpdateManyAsync(filter, update);
            return updateResult.MatchedCount == updateResult.ModifiedCount;
        }
    }
}
