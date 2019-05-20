using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomWithAVue.Data.Entities;

namespace RoomWithAVue.Data
{
  public class AccomodationSeeder
  {
    private readonly AccomodationContext _ctx;

    public AccomodationSeeder(AccomodationContext ctx)
    {
      _ctx = ctx;
    }

    public async Task<bool> SeedDatabaseAsync()
    {
      await _ctx.Database.EnsureCreatedAsync();

      if (_ctx.Rooms.Any()) return true;

      var rooms = CreateRooms();
      _ctx.Rooms.AddRange(rooms);
      var customers = CreateData();
      _ctx.Customers.AddRange(customers);

      _ctx.Stays.AddRange(new List<Stay>()
      {
        new Stay()
        {
          Departure = DateTime.Today.AddDays(3),
          Room = rooms.ElementAt(0),
          Rate = rooms.ElementAt(0).Rate,
          Guest = customers.ElementAt(0)
        },
        new Stay()
        {
          Departure = DateTime.Today.AddDays(2),
          Room = rooms.ElementAt(1),
          Rate = rooms.ElementAt(1).Rate,
          Guest = customers.ElementAt(1)
        },
      });

      return await _ctx.SaveChangesAsync() > 0;
    }

    private IEnumerable<Room> CreateRooms()
    {
      return new List<Room>()
      {
        new Room()
        {
          Title  = "The Varsity",
          MaxGuests = 2,
          Rate = 149.00,
          Description = "Romatic room with view of downtown Atlanta",
          Images = new List<RoomImage>()
          {
            new RoomImage() { Url = "/img/varsity-1.jpg", Description = "Main Room" },
            new RoomImage() { Url = "/img/varsity-2.jpg", Description = "Couch" },
            new RoomImage() { Url = "/img/varsity-3.jpg", Description = "Bathroom" }
          }
        },
        new Room()
        {
          Title  = "The Omni",
          MaxGuests = 3,
          Rate = 199.00,
          Description = "Romatic room with view of downtown Atlanta",
          Images = new List<RoomImage>()
          {
            new RoomImage() { Url = "/img/omni-1.jpg", Description = "Main Room" },
            new RoomImage() { Url = "/img/omni-2.jpg", Description = "Couch" },
            new RoomImage() { Url = "/img/omni-3.jpg", Description = "Bathroom" }
          }
        }
      };
    }

    private IEnumerable<Customer> CreateData()
    {
      return new List<Customer>()
      {
        new Customer()
        {
          Name = "ABC Products",
          Contact = "Jake Smith",
          Phone = "(404) 555-3030",
          Address = new Address()
          {
            AddressLine1 = "123 Main Street",
            CityTown = "Atlanta",
            StateProvince = "GA",
            PostalCode = "12345",
            Country = "USA"
          }
        },
        new Customer()
        {
          Name = "XYZ Supermarket",
          Contact = "Tom Paul",
          Phone = "(770) 555-3030",
          Address = new Address()
          {
            AddressLine1 = "1000 Peachtree St.",
            CityTown = "Atlanta",
            StateProvince = "GA",
            PostalCode = "23456",
            Country = "USA"
          }
        }
      };


    }
  }
}
