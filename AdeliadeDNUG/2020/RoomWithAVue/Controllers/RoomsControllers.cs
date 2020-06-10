using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomWithAVue.Data;

namespace RoomWithAVue.Controllers
{
  [Route("api/rooms")]
  public class RoomsControllers : Controller
  {
    private readonly AccomodationContext _ctx;
    private readonly ILogger<RoomsControllers> _logger;

    public RoomsControllers(AccomodationContext ctx, ILogger<RoomsControllers> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Get()
    {
      return Ok(_ctx.Rooms.Include(r => r.Images).OrderBy(r => r.Rate).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var room = _ctx.Rooms.Include(r => r.Images).Where(r => r.Id == id).FirstOrDefault();

      if (room == null) return NotFound();

      return Ok(room);
    }
  }
}
