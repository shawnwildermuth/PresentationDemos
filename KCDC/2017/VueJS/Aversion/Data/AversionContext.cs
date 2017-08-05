using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aversion.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aversion.Data
{
  public class AversionContext : DbContext
  {
    private readonly IConfiguration _config;

    public AversionContext(DbContextOptions options, IConfiguration config) : base(options)
    {
      _config = config;
    }

    public DbSet<Customer>  Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(_config.GetConnectionString("AversionConnection"));
    }
  }
}
