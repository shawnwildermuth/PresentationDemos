using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;

namespace MyCodeCamp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();

      SeedDatabaseAsync(host).Wait();

      host.Run();
    }

    private static async Task SeedDatabaseAsync(IWebHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
      using (var scope = scopeFactory.CreateScope())
      {
        var initializer = scope.ServiceProvider.GetService<CampDbInitializer>();
        await initializer.SeedAsync();

        var identityInit = scope.ServiceProvider.GetService<CampIdentityInitializer>();
        await identityInit.SeedAsync();

      }
    }


    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
