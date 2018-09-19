using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aversion.Data;
using Aversion.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aversion.Controllers
{
  [Route("api/v{version:apiVersion}/customers/{customerId}/[controller]")]
  public class OrdersController : Controller
  {
    private readonly AversionContext _ctx;

    public OrdersController(AversionContext ctx)
    {
      _ctx = ctx;
    }

    [HttpGet]
    public IActionResult Get(int customerId)
    {
      return Ok(_ctx.Orders
        .Where(o => o.Customer.Id == customerId)
        .Include(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(o => o.OrderNumber)
        .ToList());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public IActionResult Get(int customerId, int id)
    {
      var order = _ctx.Orders
        .Where(o => o.Customer.Id == customerId && o.Id == id)
        .Include(o => o.Items).ThenInclude(i => i.Product)
        .FirstOrDefault();

      if (order == null) return NotFound();

      return Ok(new {
        version = Request.HttpContext.GetRequestedApiVersion().ToString(),
        result = order
      });
    }

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post(int customerId, [FromBody]Order model)
    {
      if (ModelState.IsValid)
      {
        var customer = _ctx.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        if (customer == null) return NotFound();

        customer.Orders.Add(model);

        if (await _ctx.SaveChangesAsync() > 0)
        {
          return Created($"/api/customers/{customerId}/orders/{model.Id}", model);
        }
      }

      return BadRequest();
    }

  }
}
