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
  [ApiVersion("1.1")]
  public class Customers2Controller : CustomersController
  {
    public Customers2Controller(AversionContext ctx) : base(ctx)
    {
    }

    public override IActionResult Get()
    {
      return Ok(_ctx.Customers
        .Include(c => c.Address)
        .Include(c => c.Orders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(c => c.Name)
        .ToList());
    }

    public override IActionResult Get(int id)
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
  }
}
