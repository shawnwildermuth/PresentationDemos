using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyCodeCamp.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MyCodeCamp.Data
{
  public class CampContext : IdentityDbContext
  {
    private readonly IConfiguration _config;

    public CampContext(DbContextOptions options, IConfiguration config)
      : base(options)
    {
      _config = config;
    }

    public DbSet<Camp> Camps { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Talk> Talks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Camp>()
        .Property(c => c.Moniker)
        .IsRequired();
      builder.Entity<Camp>()
        .Property(c => c.RowVersion)
        .ValueGeneratedOnAddOrUpdate()
        .IsConcurrencyToken();
      builder.Entity<Speaker>()
        .Property(c => c.RowVersion)
        .ValueGeneratedOnAddOrUpdate()
        .IsConcurrencyToken();
      builder.Entity<Talk>()
        .Property(c => c.RowVersion)
        .ValueGeneratedOnAddOrUpdate()
        .IsConcurrencyToken();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseInMemoryDatabase("MyCodeCamp");
    }
  }
}
