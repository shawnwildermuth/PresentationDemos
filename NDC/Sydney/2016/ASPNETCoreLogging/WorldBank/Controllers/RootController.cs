using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorldBank.Data;

namespace WorldBank.Controllers
{
  public class RootController : Controller
  {
    private BankContext _ctx;
    private ILogger<BankContext> _logger;

    public RootController(BankContext ctx, ILogger<BankContext> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    public IActionResult Index()
    {
      _logger.LogInformation("About to show categories");

      return View(_ctx.Projects
        .GroupBy(p => p.RegionName)
        .Select(p => p.Key)
        .OrderBy(p => p)
        .ToList());
    }

    public IActionResult Region(string id)
    {
      using (var scope = _logger.BeginScope("Showing Region"))
      {

        var result = _ctx.Projects
          .Include(p => p.Documents)
          .Where(p => p.RegionName == id)
          .OrderByDescending(p => p.ApprovalDate)
          .ToList();

        if (_logger.IsEnabled(LogLevel.Information))
        {
          foreach (var r in result)
          {
            _logger.LogInformation($"Found {r.ProjectName} project");
          }
        }

        return View(result);
      }
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
