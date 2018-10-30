using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Saudi.Controllers
{
  public class RootController : Controller
  {
    private readonly IConfiguration _config;

    public RootController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      //throw new InvalidOperationException("Bad things happen to good developers");

      ViewBag.Color = _config["Colors:Favorite"];

      return View();
    }
  }
}
