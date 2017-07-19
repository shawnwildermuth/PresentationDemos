using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FunWithCore.Controllers
{
  public class RootController : Controller
  {
    [HttpGet("")]
    public IActionResult Index()
    {
      throw new InvalidProgramException("Bad things happen to good developers");

      return View();
    }
  }
}
