using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomWithAVue.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RoomWithAVue.Data
{
  public class AccomodationContext : DbContext
  {
    private readonly IConfiguration _config;

    public AccomodationContext(DbContextOptions options, IConfiguration config) : base(options)
    {
      _config = config;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Stay> Stays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(_config.GetConnectionString("AccomodationConnection"));
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    {
      base.OnModelCreating(bldr);

      bldr.Entity<Room>()
        .HasData(AccomodationSeed.CreateRooms());

      bldr.Entity<Customer>()
        .HasData(AccomodationSeed.CreateCustomers());

      bldr.Entity<Address>()
        .HasData(AccomodationSeed.CreateAddresses());

      bldr.Entity<RoomImage>()
        .HasData(AccomodationSeed.CreateRoomImages());
    }
  }
}
