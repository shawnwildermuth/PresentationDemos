using Dealership.APIs;
using Dealership.Data;
using WilderMinds.MinimalApiDiscovery;
using FluentValidation;
using Dealership.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Repo>();

builder.Services.AddControllers();

builder.Services.AddAuthentication()
  .AddJwtBearer();

builder.Services.AddValidatorsFromAssemblyContaining<CarValidator>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapApis();

app.MapGet("api/valid",
  (IConfiguration config) => config["Valid"]);
app.Run();

public partial class Program { }
