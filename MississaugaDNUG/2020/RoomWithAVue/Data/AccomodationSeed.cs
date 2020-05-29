using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomWithAVue.Data.Entities;

namespace RoomWithAVue.Data
{
  public static class AccomodationSeed
  {
    public static RoomImage[] CreateRoomImages()
    {
      return new RoomImage[]
      {
        new RoomImage() { Id = 1, RoomId = 1, Url = "/img/varsity-1.jpg", Description = "Main Room" },
        new RoomImage() { Id = 2, RoomId = 1, Url = "/img/varsity-2.jpg", Description = "Couch" },
        new RoomImage() { Id = 3, RoomId = 1, Url = "/img/varsity-3.jpg", Description = "Bathroom" },
        new RoomImage() { Id = 4, RoomId = 2, Url = "/img/omni-1.jpg", Description = "Main Room" },
        new RoomImage() { Id = 5, RoomId = 2, Url = "/img/omni-2.jpg", Description = "Couch" },
        new RoomImage() { Id = 6, RoomId = 2, Url = "/img/omni-3.jpg", Description = "Bathroom" }
      };
    }

    public static IEnumerable<Room> CreateRooms()
    {
      return new List<Room>()
      {
        new Room()
        {
          Id = 1,
          Title  = "The Varsity",
          MaxGuests = 2,
          Rate = 149.00,
          Description = "Romatic room with view of downtown Atlanta"
        },
        new Room()
        {
          Id = 2,
          Title  = "The Omni",
          MaxGuests = 3,
          Rate = 199.00,
          Description = "Romatic room with view of downtown Atlanta"
        }
      };
    }

    public static IEnumerable<Customer> CreateCustomers()
    {
      return new List<Customer>()
      {
        new Customer()
        {
          Id = 1,
          Name = "ABC Products",
          Contact = "Jake Smith",
          Phone = "(404) 555-3030",
          AddressId = 1
        },
        new Customer()
        {
          Id = 2,
          Name = "XYZ Supermarket",
          Contact = "Tom Paul",
          Phone = "(770) 555-3030",
          AddressId = 2
        }
      };
    }

    public static IEnumerable<Address> CreateAddresses()
    {
      return new List<Address>()
      {
        new Address()
        {
          Id = 1,
          AddressLine1 = "123 Main Street",
          CityTown = "Atlanta",
          StateProvince = "GA",
          PostalCode = "12345",
          Country = "USA"
        },
        new Address()
        {
          Id = 2,
          AddressLine1 = "1000 Peachtree St.",
          CityTown = "Atlanta",
          StateProvince = "GA",
          PostalCode = "23456",
          Country = "USA"
        }
      };
    }
  }
}
