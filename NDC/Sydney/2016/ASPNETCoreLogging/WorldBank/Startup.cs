using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
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
      ILoggerFactory loggingFactory)
    {
      var filename = Path.Combine(env.ContentRootPath, "log/log.log");

      Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.File(filename, LogEventLevel.Information)
        .CreateLogger();

      if (env.IsDevelopment())
      {
        loggingFactory.AddSerilog();

        loggingFactory
          .AddConsole(_config.GetSection("Logging"));

        loggingFactory.AddDebug(LogLevel.Information);

        app.UseDeveloperExceptionPage();
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
