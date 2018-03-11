using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunInCharlotte
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(CreateConfig)
                .UseStartup<Startup>()
                .Build();

    private static void CreateConfig(WebHostBuilderContext ctx, 
      IConfigurationBuilder bldr)
    {
      bldr.AddJsonFile("appconfig.json", true, true);
      bldr.AddEnvironmentVariables();
    }
  }
}
