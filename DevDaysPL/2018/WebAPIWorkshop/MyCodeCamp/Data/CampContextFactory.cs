using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyCodeCamp.Data
{
  public class CampContextFactory : IDesignTimeDbContextFactory<CampContext>
  {
    public CampContext CreateDbContext(string[] args)
    {
      // Create a configuration 
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      return new CampContext(new DbContextOptionsBuilder<CampContext>().Options, config);
    }
  }
}
