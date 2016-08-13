using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunWithASPNETCore.Controllers;
using Microsoft.EntityFrameworkCore;

namespace FunWithASPNETCore.Data
{
  public class FunContext : DbContext
  {
    public FunContext(DbContextOptions opt) : base(opt)
    {
      this.Database.EnsureDeleted();
      this.Database.EnsureCreated();
    }

    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DDDFun;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }

  }
}
