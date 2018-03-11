using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FunInCharlotte.Controllers
{
  
  public class PeopleController : Controller
  {
    [HttpGet("api/people")]
    public IActionResult Get()
    {
      return Ok(new People[] {
        new People() { Name = "Shawn", Last = "Wildermuth" }
      });
    }
  }
}
