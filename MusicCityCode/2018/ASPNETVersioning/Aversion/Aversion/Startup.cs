using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aversion.Controllers;
using Aversion.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
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
      services.AddApiVersioning(cfg =>
      {
        cfg.DefaultApiVersion = new ApiVersion(1, 1);
        cfg.AssumeDefaultVersionWhenUnspecified = true;
        cfg.ReportApiVersions = true;
        //cfg.ApiVersionReader = new HeaderApiVersionReader("ver");
        cfg.ApiVersionReader = new UrlSegmentApiVersionReader();
        cfg.Conventions.Controller<CustomersV2Controller>()
          .HasApiVersion(2, 0)
          .Action(c => c.Get()).MapToApiVersion(2, 0);
        cfg.Conventions.Controller<CustomersController>()
          .HasDeprecatedApiVersion(1, 0)
          .HasApiVersion(1, 1)
          .Action(c => c.Get()).MapToApiVersion(1, 0)
          .Action(c => c.GetV11()).MapToApiVersion(1, 1);


      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseMvc();

      using (var scope = app.ApplicationServices.CreateScope())
      {
        var seeder = scope.ServiceProvider.GetService<AversionSeeder>();
        seeder.SeedDatabaseAsync().Wait();
      }
    }
  }
}
