using System;

namespace Contact.API.Infrastructure.Exceptions
{
  public class ContactDomainException:Exception
  {
    public ContactDomainException()
    { }

    public ContactDomainException(string message)
      : base(message)
    { }

    public ContactDomainException(string message, Exception innerException)
      : base(message, innerException)
    { }
  }
}
