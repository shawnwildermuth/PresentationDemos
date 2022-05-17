using FunWithGrpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGrpcService<WeatherGrpcService>();

app.MapRazorPages();

app.Run();
