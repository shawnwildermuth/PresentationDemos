using Dealership.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Repo>());

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();



app.Run();
