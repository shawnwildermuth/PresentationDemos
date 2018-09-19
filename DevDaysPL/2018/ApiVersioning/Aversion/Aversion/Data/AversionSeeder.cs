using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aversion.Data.Entities;

namespace Aversion.Data
{
  public class AversionSeeder
  {
    private readonly AversionContext _ctx;

    public AversionSeeder(AversionContext ctx)
    {
      _ctx = ctx;
    }

    public async Task<bool> SeedDatabaseAsync()
    {
      await _ctx.Database.EnsureCreatedAsync();

      if (_ctx.Products.Any()) return true;

      _ctx.Customers.AddRange(CreateData());

      return await _ctx.SaveChangesAsync() > 0;
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
          },
          Orders = new List<Order>()
          {
            new Order()
            {
              OrderDate = DateTime.Now,
              OrderNumber = "12345",
              Items = new List<OrderItem>()
              {
                new OrderItem()
                {
                  Price = 19.95m,
                  Quantity = 12,
                  Product = new Product()
                  {
                    Name = "Widget, Small",
                    ListPrice = 25
                  }
                },
                new OrderItem()
                {
                  Price = 9.95m,
                  Quantity = 25,
                  Product = new Product()
                  {
                    Name = "Widget, Large",
                    ListPrice = 15
                  }
                },
              }
            }
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
          },
          Orders = new List<Order>()
          {
            new Order()
            {
              OrderDate = DateTime.Now,
              OrderNumber = "12346",
              Items = new List<OrderItem>()
              {
                new OrderItem()
                {
                  Price = 4.95m,
                  Quantity = 50,
                  Product = new Product()
                  {
                    Name = "AA Batteries, 12 pack",
                    ListPrice = 6
                  }
                },
                new OrderItem()
                {
                  Price = 5.50m,
                  Quantity = 25,
                  Product = new Product()
                  {
                    Name = "AAA Batteries, 12 pack",
                    ListPrice = 6
                  }
                },
              }
            }
          }
        }
      };
    }
  }
}
