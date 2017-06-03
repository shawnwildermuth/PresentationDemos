using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldBank.Data;

namespace WorldBank.Controllers
{
  public class RootController : Controller
  {
    private BankContext _ctx;

    public RootController(BankContext ctx)
    {
      _ctx = ctx;
    }

    public IActionResult Index()
    {
      return View(_ctx.Projects
        .GroupBy(p => p.RegionName)
        .Select(p => p.Key)
        .OrderBy(p => p)
        .ToList());
    }

    public IActionResult Region(string id)
    {
      return View(_ctx.Projects
        .Include(p => p.Documents)
        .Where(p => p.RegionName == id)
        .OrderByDescending(p => p.ApprovalDate)
        .ToList());
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
