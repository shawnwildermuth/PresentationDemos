using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunWithCore
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, builder) =>
            {
              builder.SetBasePath(Environment.CurrentDirectory);
              builder.AddJsonFile("config.json", true, true)
                     //.AddIniFile("foo.ini", true)
                     // .AddJsonFile("config.developer.json", false)
                     //.AddXmlFile("foo.xml", false)
                     .AddEnvironmentVariables();
              
            })
            .UseStartup<Startup>()
            .Build();
  }
}
