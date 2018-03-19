using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomWithAVue.Data;
using RoomWithAVue.ViewModels;

namespace RoomWithAVue.Controllers
{
  public class RootController : Controller
  {
    private readonly AccomodationContext _ctx;
    ILogger<RootController> _logger;

    public RootController(AccomodationContext ctx, ILogger<RootController> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
      var query = from r in _ctx.Rooms.Include(rm => rm.Images)
                  orderby r.Title
                  select r;

      return View(query.ToList());
    }

    [HttpGet("contact")]
    public IActionResult Contact()
    {
      return View();
    }

    [HttpPost("api/contact")]
    public IActionResult ContactUs([FromBody] ContactViewModel vm)
    {
      if (ModelState.IsValid)
      {
        // TODO Send Mail
        return Ok("Sent");
      }

      return BadRequest();
    }
  }
}