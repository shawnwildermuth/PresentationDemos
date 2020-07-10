using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace WhatIsGrpc.Services
{
  public class WeatherGrpcService : WeatherService.WeatherServiceBase
  {
    public override Task<WeatherResponse> GetWeather(WeatherRequest request, ServerCallContext context)
    {
      return Task.FromResult(GetWeatherData());
    }

    private WeatherResponse GetWeatherData()
    {
      var rnd = new Random();

      var res = new WeatherResponse
      {
        Temperature = (float)(rnd.NextDouble() * 100.0),
        WindSpeed = (float)(rnd.NextDouble() * 50.0),
        RainAmount = (float)(rnd.NextDouble() * 10.0)
      };

      return res;
    }

    public override async Task GetWeatherStream(WeatherRequest request, 
      IServerStreamWriter<WeatherResponse> responseStream, 
      ServerCallContext context)
    {
      for (var x = 0; x < 10; ++x)
      {
        await responseStream.WriteAsync(GetWeatherData());
        await Task.Delay(500);
      }
    }

    public override Task<Readings> GetWeatherReadings(WeatherRequest request, ServerCallContext context)
    {
      var readings = new WeatherResponse[]
      {
        GetWeatherData(),
        GetWeatherData(),
        GetWeatherData(),
        GetWeatherData(),
        GetWeatherData(),
      };

      var returnValue = new Readings();
      returnValue.TheReadings.Add(readings);

      return Task.FromResult(returnValue);
    }
  }
}
