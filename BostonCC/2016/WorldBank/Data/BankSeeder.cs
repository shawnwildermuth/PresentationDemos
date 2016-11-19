using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorldBank.Data
{
  public class BankSeeder
  {
    private BankContext _ctx;
    private IHostingEnvironment _env;
    private ILogger<BankSeeder> _logger;

    public BankSeeder(BankContext ctx, IHostingEnvironment env, ILogger<BankSeeder> logger)
    {
      _ctx = ctx;
      _env = env;
      _logger = logger;

    }

    public async Task SeedDatabaseAsync()
    {
      await _ctx.Database.EnsureCreatedAsync();

      // Shortcut
      if (_ctx.Projects.Any())
      {
        // Log
        _logger.LogInformation("Seeding not necessary");
        return;
      }

      try
      {
        using (_logger.BeginScope("Seeding DB"))
        {
          var lines = File.ReadAllText(Path.Combine(_env.ContentRootPath, @"Data\src.data"));
          var doc = JArray.Parse(lines);
          foreach (JObject src in doc)
          {
            var project = new Project();
            project.Abstract = GetAbstract(src);
            project.ApprovalDate = src.Value<DateTime>("boardapprovaldate");
            project.Borrower = src.Value<string>("borrower");
            project.ClosingDate = src.Value<DateTime>("closingdate");
            project.Country = src.Value<string>("countryname");
            project.GrantAmount = src.Value<int>("grantamt");
            project.LendingInstrument = src.Value<string>("lendinginstr");
            project.LendingProjectCost = src.Value<long>("lendprojectcost");
            project.ProductLine = src.Value<string>("prodlinetext");
            project.ProjectName = src.Value<string>("project_name");
            project.RegionName = src.Value<string>("regionname");
            project.Source = src.Value<string>("source");
            project.Status = src.Value<string>("status");
            project.TotalAmount = src.Value<int>("totalamt");
            project.TotalCommitmentAmount = src.Value<int>("totalcommamt");
            project.Url = src.Value<string>("url");
            project.Documents = GetDocuments(src);

            _ctx.Add(project);

            if (_logger.IsEnabled(LogLevel.Information))
            {
              _logger.LogInformation($"Adding {project.ProjectName}");
            }
          }
        }

        await _ctx.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error Occurred: {ex}");

        var problem = ex.Message;
      }
    }

    private string GetAbstract(JObject src)
    {
      var @abstract = src["project_abstract"];
      if (@abstract != null) return (string)@abstract["cdata"];
      return "";
    }

    private ICollection<ProjectDocument> GetDocuments(JObject src)
    {
      var list = new List<ProjectDocument>();
      var docs = (JArray)src["projectdocs"];
      if (docs != null)
      {
        foreach (var doc in docs)
        {
          list.Add(new ProjectDocument()
          {
            Description = doc.Value<string>("DocTypeDesc"),
            Url = doc.Value<string>("DocURL")
          });
        }
      }
      return list;
    }
  }

}
