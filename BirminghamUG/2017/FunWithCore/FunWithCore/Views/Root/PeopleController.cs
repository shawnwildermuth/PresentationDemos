using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FunWithCore.Views.Root
{
  [Route("api/[controller]")]
  public class PersonController : Controller
  {
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        _logger.LogWarning("Trying to return people");
        var result = new List<Person>();

        result.Add(new Person()
        {
          Id = 1,
          Name = "Shawn"
        });



        return Ok(result);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Bad things: {ex}");
        return BadRequest("Bad things");
      }

    }
  }
}
