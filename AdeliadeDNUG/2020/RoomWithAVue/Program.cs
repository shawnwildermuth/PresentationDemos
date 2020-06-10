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
using RoomWithAVue.Data;

namespace RoomWithAVue
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildHost(args).Run();
    }

    public static IWebHost BuildHost(string[] args)
    {
      var host = WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((ctx, cfg) =>
        {
          cfg.Sources.Clear();
          cfg.AddJsonFile(Path.Combine(ctx.HostingEnvironment.ContentRootPath, "config.json"), false, true);
        })
        .UseStartup<Startup>()
        .Build();

      return host;
    }
  }
}
