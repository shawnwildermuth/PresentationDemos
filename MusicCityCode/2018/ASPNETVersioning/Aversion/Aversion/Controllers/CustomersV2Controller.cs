using Aversion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aversion.Controllers
{
  [Route("/api/v{version:apiVersion}/customers")]
  //[ApiVersion("2.0")]
  public class CustomersV2Controller : CustomersController
  {
    public CustomersV2Controller(AversionContext ctx) : base(ctx)
    {

    }

    [HttpGet]
    public override IActionResult Get()
    {
      var result = _ctx.Customers
        .Include(c => c.Address)
        .Include(c => c.Orders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
        .OrderBy(c => c.Name)
        .ToList();

      return Ok(new
      {
        count = result.Count(),
        results = result
      });
    }
  }
}
