using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
  public class BaseController : Controller
  {
    protected int AccountId
    {
      get
      {
        return Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
      }
    }
  }
}
