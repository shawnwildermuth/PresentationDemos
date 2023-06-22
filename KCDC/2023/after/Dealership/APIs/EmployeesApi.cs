using Dealership.Data;
using Dealership.Validators;
using WilderMinds.MinimalApiDiscovery;

namespace Dealership.APIs;

public class EmployeesApi : IApi
{
  public void Register(WebApplication app)
  {
    var grp = app.MapGroup("api/employees")
      .RequireAuthorization();

    grp.MapGet("", GetEmployees);

    grp.MapGet("{id:int}", (Repo repo, int id) =>
    {
      try
      {
        var results = repo.Employees.Find(e => e.Id == id);
        return Results.Ok(results);
      }
      catch (Exception)
      {
        return Results.BadRequest();
      }
    })
      //.RequireAuthorization()
      .CacheOutput()
      .WithName("Foo");

    //grp.MapGet("{id:int}/cars", (Repo repo, int id) =>
    //{
    //  try
    //  {
    //    var results = repo.Cars.Where(c => c?.SalesAssociate?.Id == id)
    //      .ToList(); ;
    //    return Results.Ok(results);
    //  }
    //  catch (Exception)
    //  {
    //    return Results.BadRequest();
    //  }
    //});

    grp.MapPost("", (Employee employee) =>
    {
      return Results.CreatedAtRoute("Foo", employee);
    })
      .AddEndpointFilter<ValidationEndpoint<Employee>>();
;
  }

  public static IResult GetEmployees(HttpContext ctx, Repo repo)
  {
    try
    {
      var results = repo.Employees.ToList();
      return Results.Ok(results);
    }
    catch (Exception)
    {
      return Results.BadRequest();
    }
  }
}
