using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aversion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aversion.Controllers
{
  [Route("api/customers")]
  //[ApiVersion("2.0")]
  [ApiController]
  public class CustomersV2Controller : CustomersController
  {
    public CustomersV2Controller(AversionContext ctx) : base(ctx)
    {
    }

    [HttpGet("{id}")]
    public override IActionResult Get(int id)
    {
      var customer = _ctx.Customers
        .Include(c => c.Address)
        .Include(c => c.Orders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(c => c.Name)
        .Where(c => c.Id == id)
        .FirstOrDefault();

      if (customer == null) return NotFound();

      return Ok(new
      {
        ApiVersion = Request.HttpContext.GetRequestedApiVersion().ToString(),
        OrderCount = customer.Orders.Count(),
        Result = customer
      });
    }
  }
}
