using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Saudi.Controllers
{
  [Route("api/people")]
  public class PeopleController : ControllerBase
  {
    public ActionResult Get()
    {
      return Ok(new[] {
      new {
        Name = "Shawn",
        Birthday = DateTime.Today
        }
      });
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      return Ok(
      new
      {
        Id = id,
        Name = "Shawn",
        Birthday = DateTime.Today
      }
      );
  }
}
}
