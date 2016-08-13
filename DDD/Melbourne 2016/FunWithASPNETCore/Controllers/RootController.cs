using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FunWithASPNETCore.Controllers
{
  public class RootController : Controller
  {
    private IConfigurationRoot _config;

    public RootController(IConfigurationRoot config)
    {
      _config = config;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      //throw new InvalidOperationException("Bad things happen to good developers");

      ViewBag.color = _config["colors:favoriteColor"];
      return View();
    }
  }
}
