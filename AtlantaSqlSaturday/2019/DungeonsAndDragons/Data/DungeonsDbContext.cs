using System;
using DungeonsAndDragons.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DungeonsAndDragons
{
  public partial class DungeonsDbContext : DbContext
  {
    private readonly IConfiguration _config;

    public DungeonsDbContext(DbContextOptions<DungeonsDbContext> options, IConfiguration config)
        : base(options)
    {
      _config = config;
    }

    public DbSet<CharacterClass> Classes { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<ChoiceItem> ChoiceItems { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Spell> Spells { get; set; }
    public DbSet<Weapon> Weapons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DungeonDb"));
      }
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    {

    }
  }
}
