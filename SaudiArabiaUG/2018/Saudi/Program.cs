using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Saudi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(DoConfig)
            .UseStartup<Startup>();

    private static void DoConfig(WebHostBuilderContext ctx, IConfigurationBuilder bldr)
    {
      bldr.Sources.Clear();

      bldr.AddJsonFile("appsettings.json")
          .AddEnvironmentVariables();
    }
  }
}
