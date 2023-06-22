using System.Text.Json;
using Dealership.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Dealership.Tests.IntegrationTests;

public class CarsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;

  public CarsIntegrationTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task CanGetCarsAgain()
  {
    var client = _factory.CreateClient();

    var result = await client.GetAsync("/api/cars");

    Assert.NotNull(result);
    Assert.True(result.IsSuccessStatusCode);

    var content = await result.Content.ReadAsStringAsync();

    Assert.NotEmpty(content);

    var cars = JsonSerializer
      .Deserialize<IEnumerable<Vehicle>>(content);

    Assert.NotNull(cars);
    Assert.NotEmpty(cars);

  }

  [Fact]
  public async Task CanGetConfigValue()
  {
    var client = _factory.CreateClient();

    var result = await client.GetStringAsync("/api/valid");

    Assert.Equal("True", result);
  }
}
