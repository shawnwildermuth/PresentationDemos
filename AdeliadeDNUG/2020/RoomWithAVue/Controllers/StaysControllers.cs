using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomWithAVue.Data;
using RoomWithAVue.Data.Entities;

namespace RoomWithAVue.Controllers
{
  [Route("api/customers/{custid}/stays")]
  public class StaysControllers : Controller
  {
    private readonly AccomodationContext _ctx;
    private readonly ILogger<StaysControllers> _logger;

    public StaysControllers(AccomodationContext ctx, ILogger<StaysControllers> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Get(int custId)
    {
      var stays = _ctx.Stays.Include(s => s.Guest).Include(s => s.Room).ThenInclude(r => r.Images).Where(s => s.Guest.Id == custId).ToList();

      return Ok(stays);
    }

    [HttpPost]
    public IActionResult Post(int custId, [FromBody]Stay stay)
    {
      if (ModelState.IsValid)
      {
        var customer = _ctx.Customers.Where(c => c.Id == custId).FirstOrDefault();
        if (customer == null) return NotFound();
        var room = _ctx.Rooms.Where(r => r.Id == stay.Room.Id).FirstOrDefault();
        if (room == null) return NotFound();

        stay.Guest = customer;
        stay.Room = room;

        _ctx.Stays.Add(stay);

        _ctx.SaveChanges();

        return Created($"api/customers/{custId}/stays/{stay.Id}", stay);
      }

      return BadRequest();

    }

    [HttpPut("{id}")]
    public IActionResult Put(int custId, int id, [FromBody]Stay stay)
    {
      if (ModelState.IsValid)
      {
        var oldStay = _ctx.Stays.Where(s => s.Id == id && s.Guest.Id == custId).FirstOrDefault();
        if (oldStay == null) return NotFound();
        var room = _ctx.Rooms.Where(r => r.Id == stay.Room.Id).FirstOrDefault();
        if (room == null) return NotFound();

        oldStay.Arrival = stay.Arrival;
        oldStay.Departure = stay.Departure;
        oldStay.Rate = stay.Rate;
        oldStay.Room = room;

        _ctx.SaveChanges();

        return Ok(oldStay);
      }

      return BadRequest();

    }

  }
}
