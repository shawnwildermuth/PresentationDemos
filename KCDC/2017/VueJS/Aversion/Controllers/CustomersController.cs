using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aversion.Data;
using Aversion.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aversion.Controllers
{
  [Route("api/[controller]")]
  public class CustomersController : Controller
  {
    private readonly AversionContext _ctx;

    public CustomersController(AversionContext ctx)
    {
      _ctx = ctx;
    }

    // GET api/values
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_ctx.Customers
        .Include(c => c.Address)
        .Include(c => c.Orders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(c => c.Name)
        .ToList());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var customer = _ctx.Customers
        .Include(c => c.Address)
        .Include(c => c.Orders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(c => c.Name)
        .Where(c => c.Id == id)
        .FirstOrDefault();

      if (customer == null) return NotFound();

      return Ok(customer);
    }

    // POST api/values
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
