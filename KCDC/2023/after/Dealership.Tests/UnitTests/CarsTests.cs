using Dealership.Controllers;
using Dealership.Data;
using Dealership.Validators;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Dealership.Tests.UnitTests;

public class CarsTests
{
  private CarsController _ctrl;

  public CarsTests()
  {
    var validator = new CarValidator();
    var repo = new Repo();
    var logger = new NullLogger<CarsController>();
    var config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

    _ctrl = new CarsController(logger, repo, validator, config);
  }

  [Fact]
  public void CanGetCars()
  {
    var result = _ctrl.Get();
    Assert.IsAssignableFrom<Ok<List<Vehicle>>>(result);

    var value = ((Ok<List<Vehicle>>)result).Value;

    Assert.NotNull(value);
    Assert.NotEmpty(value);
    Assert.True(value.Count == 25);
  }
}
