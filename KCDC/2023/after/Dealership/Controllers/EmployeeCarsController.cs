using Dealership.Data;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Controllers;

[Route("api/employees/{empId:int}/cars")]
public class EmployeeCarsController : ControllerBase
{
  public EmployeeCarsController()
  {

  }

  [HttpGet]
  public IResult Get(Repo repo, int empId)
  {
    var result = repo.Cars
      .Where(c => c.SalesAssociate is not null
      && c.SalesAssociate.Id == empId)
      .ToList();

    return Results.Ok(result);
  }

  public IResult Post(Repo repo, int empId, [FromBody] Vehicle model)
  {
    var employee = repo.Employees.Find(e => e.Id == empId);
    //... This no work

    return Results.Created("/api/cars", model);
  }
}
