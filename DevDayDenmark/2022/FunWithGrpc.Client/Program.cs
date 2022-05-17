using Grpc.Net.Client;
using WeatherStations;
using static WeatherStations.WeatherService;

var channel = GrpcChannel.ForAddress("https://localhost:5001");

var client = new WeatherServiceClient(channel);

while (true)
{

  var req = new WeatherRequest() { StationId = 1 };

  //var response = client.GetWeather(req);

  var response = client.GetWeathers(req);

  Console.WriteLine(response);

  Console.ReadKey();
}