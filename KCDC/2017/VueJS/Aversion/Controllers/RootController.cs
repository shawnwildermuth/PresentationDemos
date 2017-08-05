using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aversion.Controllers
{
  public class RootController : Controller
  {

    ILogger<RootController> _logger;

    public RootController(ILogger<RootController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      
      return View();
    }
  }
}