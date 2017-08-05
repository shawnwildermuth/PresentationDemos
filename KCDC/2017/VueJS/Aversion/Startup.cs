using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aversion.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aversion
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AversionContext>();
      services.AddTransient<AversionSeeder>();
      services.AddMvc()
        .AddJsonOptions(j => j.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseStaticFiles();

      app.UseMvc(opt =>
      {
        opt.MapRoute("Main", "{controller}/{action}/{id?}", new { controller = "Root", action = "Index" });
      });

      using (var scope = app.ApplicationServices.CreateScope())
      {
        var seeder = scope.ServiceProvider.GetService<AversionSeeder>();
        seeder.SeedDatabaseAsync().Wait();
      }
    }
  }
}
