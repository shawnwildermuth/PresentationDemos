using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FunWithWroclaw.Controllers
{
  public class RootController : Controller
  {

    ILogger<RootController> _logger;

    public RootController(ILogger<RootController> logger)
    {
      _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
      ViewBag.Title = "Hello from Wroclaw, Poland at WROCSHARP";
      return View();
    }
  }
}