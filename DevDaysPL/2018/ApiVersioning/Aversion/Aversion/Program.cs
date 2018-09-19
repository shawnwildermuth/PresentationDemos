using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aversion.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aversion
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = BuildWebHost(args);

      SeedDbAsync(host).Wait();

      host.Run();
    }

    private static async Task SeedDbAsync(IWebHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

      using (var scope = scopeFactory.CreateScope())
      {
        var seeder = scope.ServiceProvider.GetService<AversionSeeder>();
        await seeder.SeedDatabaseAsync();
      }
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
  }
}
