using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunWithASPNETCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FunWithASPNETCore
{
  public class Startup
  {
    private IConfigurationRoot _config;

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("config.json", false, true)
        .AddEnvironmentVariables();

      _config = builder.Build();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfigurationRoot>(_config);

      services.AddDbContext<FunContext>(ServiceLifetime.Scoped);

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      var color = _config["colors:favoriteColor"];

      if (env.IsEnvironment("Development") || env.IsEnvironment("Testing"))
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();

      app.UseMvc();
    }
  }
}
