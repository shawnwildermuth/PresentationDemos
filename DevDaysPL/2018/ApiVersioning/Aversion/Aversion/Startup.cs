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

      services.AddApiVersioning(opt =>
      {
        opt.DefaultApiVersion = new ApiVersion(2, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;

        //opt.ApiVersionReader = ApiVersionReader.Combine(
        //  new HeaderApiVersionReader("X-Version"),
        //  new MediaTypeApiVersionReader());

        

        opt.ApiVersionReader = new UrlSegmentApiVersionReader();

        opt.Conventions
          .Controller<CustomersController>()
          .HasApiVersion(1, 0)
          .HasApiVersion(1, 1)
          .Action(c => c.Get()).MapToApiVersion(2,0);

        opt.Conventions
          .Controller<CustomersV2Controller>()
          .HasApiVersion(2, 0);

        opt.Conventions
        .Controller<OrdersController>()
        .HasApiVersion(2, 0)
        .HasApiVersion(1,1);

      });

      services.AddMvc()
        .AddJsonOptions(j => j.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseMvc();
    }
  }
}
