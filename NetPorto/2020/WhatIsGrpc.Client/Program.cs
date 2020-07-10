using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using WhatIsGrpc.Services;

namespace WhatIsGrpc.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      var channel = GrpcChannel.ForAddress("https://localhost:5001");
      var client = new WeatherService.WeatherServiceClient(channel);

      while (true)
      {
        //var response = client.GetWeather(new WeatherRequest()
        //{
        //  StationId = 1
        //});

        //var response = client.GetWeatherStream(new WeatherRequest()
        //{
        //  StationId = 1
        //});

        var response = client.GetWeatherReadings(new WeatherRequest()
        {
          StationId = 1
        });

        //ProcessStream(response).Wait();
        //Console.Write($"{response}");

        foreach (var item in response.TheReadings)
        {
          Console.WriteLine($"Colletion: {item}");
        }

        Console.ReadLine();
      }
    }

    private static async Task ProcessStream(AsyncServerStreamingCall<WeatherResponse> response)
    {
      var stream = response.ResponseStream;

      while (await stream.MoveNext())
      {
        var result = stream.Current;
        Console.WriteLine($"From Stream: {result}");
      }
    }
  }
}
