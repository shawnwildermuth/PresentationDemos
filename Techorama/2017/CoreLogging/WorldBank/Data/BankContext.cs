using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WorldBank.Data
{
  public class BankContext : DbContext
  {
    private IConfigurationRoot _config;

    public BankContext(DbContextOptions options, IConfigurationRoot config) : base(options)
    {
      _config = config;
    }

    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
    }
  }
}