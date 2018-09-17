using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyCodeCamp.Data.Entities
{
  public class CampUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
