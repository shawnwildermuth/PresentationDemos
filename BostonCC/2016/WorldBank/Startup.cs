using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Loggly.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WorldBank.Data;

namespace WorldBank
{
  public class Startup
  {
    private IConfigurationRoot _config;

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", false, true)
          .AddEnvironmentVariables();

      _config = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(c => _config);
      services.AddTransient<BankSeeder>();

      services.AddDbContext<BankContext>(ServiceLifetime.Scoped);

      // Add framework services.
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
      IHostingEnvironment env, 
      BankSeeder seeder,
      ILoggerFactory logFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        logFactory.AddConsole(_config.GetSection("Logging"));
        logFactory.WithFilter(new FilterLoggerSettings()
        {
          { "Microsoft", LogLevel.Warning },
          { "System", LogLevel.Warning },
          { "WorldBank", LogLevel.Information }
        }).AddDebug();

        var config = LogglyConfig.Instance;
        config.CustomerToken = _config["Loggly:Token"];
        config.ApplicationName = "WorldBank-BostonCC2016";
        config.Transport.EndpointHostname = "logs-01.loggly.com";
        config.Transport.EndpointPort = 443;
        config.Transport.LogTransport = LogTransport.Https;

        var filename = Path.Combine(env.ContentRootPath, "log/serilog.log");
        Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.File(filename, Serilog.Events.LogEventLevel.Verbose)
          .WriteTo.Loggly(Serilog.Events.LogEventLevel.Warning)
          .CreateLogger();

        logFactory.AddSerilog();

      }
      else
      {
        app.UseExceptionHandler("/Root/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Root}/{action=Index}/{id?}");
      });

      seeder.SeedDatabaseAsync().Wait();
    }
  }
}
