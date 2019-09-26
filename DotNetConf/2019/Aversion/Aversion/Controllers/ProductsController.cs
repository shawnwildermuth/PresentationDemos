using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aversion.Data;
using Microsoft.AspNetCore.Mvc;

namespace Aversion.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly AversionContext _ctx;

    public ProductsController(AversionContext ctx)
    {
      _ctx = ctx;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_ctx.Products
        .OrderBy(o => o.Name)
        .ToList());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var product = _ctx.Products
        .Where(p => p.Id == id)
        .FirstOrDefault();

      if (product == null) return NotFound();

      return Ok(product);
    }
  }
}
