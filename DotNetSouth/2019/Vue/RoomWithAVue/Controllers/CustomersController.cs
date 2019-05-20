using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomWithAVue.Data;
using RoomWithAVue.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RoomWithAVue.Controllers
{
  [Route("api/[controller]")]
  public class CustomersController : Controller
  {
    private readonly AccomodationContext _ctx;

    public CustomersController(AccomodationContext ctx)
    {
      _ctx = ctx;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_ctx.Customers
        .Include(c => c.Address)
        .OrderBy(c => c.Name)
        .ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var customer = _ctx.Customers
        .Include(c => c.Address)
        .OrderBy(c => c.Name)
        .Where(c => c.Id == id)
        .FirstOrDefault();

      if (customer == null) return NotFound();

      return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Customer model)
    {
      if (ModelState.IsValid)
      {
        _ctx.Add(model);
        if (await _ctx.SaveChangesAsync() > 0)
        {
          return Created($"/api/customers/{model.Id}", model);
        }
      }

      return BadRequest();
    }

  }
}
