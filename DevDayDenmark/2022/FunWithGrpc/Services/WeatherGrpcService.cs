using Grpc.Core;
using WeatherStations;

namespace FunWithGrpc.Services;

public class WeatherGrpcService : WeatherService.WeatherServiceBase
{

  public override Task<WeatherResponse> GetWeather(WeatherRequest request, ServerCallContext context)
  {
    var message = Generate();

    return Task.FromResult(message);
  }

  public override Task<WeatherResponses> GetWeathers(WeatherRequest request, ServerCallContext context)
  {
    var result = new WeatherResponses();

    var readings = new WeatherResponse[]
    {
      Generate(),
      Generate(),
      Generate(),
      Generate(),
      Generate(),
    };

    result.Response.Add(readings);

    return Task.FromResult(result);

  }

  WeatherResponse Generate()
  {
    var rnd = new Random();

    var res = new WeatherResponse()
    {
      WindSpeed = (float)(rnd.NextDouble() * 10),
      Temperature = (float)(rnd.NextDouble() * 30),
      RainAmount = (float)(rnd.NextDouble() * 5),

    };

    return res;
  }
}
