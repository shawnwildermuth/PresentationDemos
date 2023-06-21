using Bogus;

namespace Dealership.Data;

public class Repo
{

  const int SEED = 12345;

  List<Vehicle>? _cars = null;
  List<Employee>? _employees = null;
  List<Lot>? _lots = null;

  public List<Lot> Lots
  {
    get
    {
      if (_lots is null)
      {
        var lotId = 0;
        var lotNum = 'A';
        var lotGen = new Faker<Lot>()
          .UseSeed(SEED)
          .RuleFor(a => a.Id, f => ++lotId)
          .RuleFor(a => a.Name, f => new String(lotNum++, 1));

        _lots = lotGen.Generate(5);
      }

      return _lots;
    }
  }

  public List<Vehicle> Cars
  {
    get
    {
      if (_cars is null)
      {

        var carId = 0;

        var cars = new Faker<Vehicle>()
          .UseSeed(SEED)
          .RuleFor(a => a.Id, f => ++carId)
          .RuleFor(a => a.Make, f => f.Vehicle.Manufacturer())
          .RuleFor(a => a.Model, f => f.Vehicle.Model())
          .RuleFor(a => a.VehicleType, f => f.Vehicle.Type())
          .RuleFor(a => a.Vin, f => f.Vehicle.Vin())
          .RuleFor(a => a.New, f => f.Random.Bool(.7f))
          .RuleFor(a => a.Year, f => f.Random.Int(1940, 2023))
          .RuleFor(a => a.Picture, f => f.Image.LoremFlickrUrl(keywords: "Car"))
          .RuleFor(a => a.SalesAssociate, f => f.PickRandom(Employees))
          .RuleFor(a => a.Lot, f => f.PickRandom(Lots));

        _cars = cars.Generate(25);

      }

      return _cars;
    }
  }
  public List<Employee> Employees
  {
    get
    {
      if (_employees is null)
      {
        var salesId = 0;

        var salesGen = new Faker<Employee>()
          .UseSeed(SEED)
          .RuleFor(a => a.Id, f => ++salesId)
          .RuleFor(a => a.FirstName, f => f.Name.FirstName())
          .RuleFor(a => a.LastName, f => f.Name.LastName())
          .RuleFor(a => a.Commission, f => f.Random.Float(0.01f, 0.25f))
          .RuleFor(a => a.Picture, f => f.Image.LoremFlickrUrl(240, 320, keywords: "headshot"));

        _employees = salesGen.Generate(15);
      }
      return _employees;
    }
  }
}
