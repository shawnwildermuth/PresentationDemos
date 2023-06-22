using Dealership.Data;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Dealership.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CarsController : ControllerBase
{
  private readonly ILogger<CarsController> _logger;
  private readonly Repo _repo;
  private readonly IValidator<Vehicle> _validator;
  private readonly IConfiguration _config;

  public CarsController(
    ILogger<CarsController> logger,
    Repo repo,
    IValidator<Vehicle> validator,
    IConfiguration config)
  {
    _logger = logger;
    _repo = repo;
    _validator = validator;
    _config = config;
  }

  [HttpGet]
  [AllowAnonymous]
  [OutputCache]
  public IResult Get()
  {
    try
    {

      return Results.Ok(_repo.Cars.ToList());

    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }

  [HttpGet("{id:int}", Name = "GetOne")]
  public IResult GetOne(int id)
  {
    try
    {
      var valid = Convert.ToBoolean(_config["Valid"]);

      var result = _repo.Cars.Where(c => c.Id == id)
        .FirstOrDefault();

      if (result is null) return Results.NotFound();

      return Results.Ok(result);

    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }

  [HttpGet("{id:int}/salesperson")]
  public IResult GetOneSalesperson(int id)
  {
    try
    {
      var result = _repo.Cars.Where(c => c.Id == id)
        .FirstOrDefault();

      if (result is null) return Results.NotFound();

      return Results.Ok(result.SalesAssociate);

    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }

  [HttpGet("{vin}")]
  public IResult GetByVin(string vin)
  {
    try
    {
      var result = _repo.Cars.Where(c => c.Vin == vin)
        .FirstOrDefault();

      if (result is null) return Results.NotFound();

      return Results.Ok(result);

    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }

  [HttpPost()]
  // If you were in the workshop, I can't 
  // explain why we needed [FromBody] on the car
  // After removing it it still worked. Odd.
  public IResult Post(Vehicle car)
  {
    try
    {

      if (car is not null)
      {
        var valid = _validator.Validate(car);

        if (!valid.IsValid)
        {
          return Results.ValidationProblem(valid.ToDictionary());
        }

        car.Id = _repo.Cars
          .OrderByDescending(c => c.Id)
          .First().Id + 1;

        _repo.Cars.Add(car);

        return Results.CreatedAtRoute("GetOne",
          new { id = car.Id },
          car);
      }

      return Results.BadRequest("Invalid Car");
    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }

  [HttpPut("{id:int}")]
  public IResult Put(int id, [FromBody] Vehicle car)
  {
    try
    {

      var old = _repo.Cars.Find(c => c.Id == id);

      if (old is null) return Results.NotFound();

      if (car is not null)
      {
        car.Adapt(old);
        // ...

        return Results.Ok(old);
      }

      return Results.BadRequest("Invalid Car");
    }
    catch (Exception ex)
    {
      _logger.LogError("Didn't work", ex);
      return Results.Problem(ex.Message);
    }
  }


  [HttpDelete("{id:int}")]
  public IResult Delete(int id)
  {
    var old = _repo.Cars.Find(c => c.Id == id);

    if (old is null) return Results.NotFound();

    _repo.Cars.Remove(old);

    return Results.Ok();
  }
}
