using Dapper;
using DungeonsAndDragons.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Data
{
  public class DungeonRepository : IDungeonRepository
  {
    private readonly DungeonsDbContext _context;
    private readonly IConfiguration _config;

    public DungeonRepository(DungeonsDbContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    private IQueryable<Character> CharacterQuery()
    {
      return _context.Characters
        .Include(c => c.Classes)
        .Include(c => c.Skills)
        .Include(c => c.Weapons)
        .Include(c => c.Spells)
        .Include(c => c.Choices)
        .ThenInclude((Choice c) => c.ChoiceItems);
    }

    public async Task<Character[]> GetCharactersAsync()
    {
      return await CharacterQuery().ToArrayAsync();
    }

    public async Task<Character> GetCharacterAsync(int id)
    {
      return await CharacterQuery()
        .Where(c => c.CharacterId == id)
        .FirstOrDefaultAsync();
    }

    public async Task<Character> GetCharacterByNameAsync(string name)
    {
      return await CharacterQuery()
        .Where(c => c.Name.ToLower() == name.ToLower())
        .FirstOrDefaultAsync();
    }

    public async Task<Character[]> GetCharactersByClassAsync(string className)
    {
      return await CharacterQuery()
        .Where(c => c.Classes.Any(n => n.ClassName.ToLower() == className.ToLower()))
        .ToArrayAsync();
    }

    public async Task<Weapon[]> GetUniqueWeaponNames()
    {
      var sql = $@"SELECT * 
  FROM (SELECT Id, 
               Name,
               CharacterId, 
               ROW_NUMBER() OVER(PARTITION by Name ORDER BY Id DESC) rn
    FROM Weapons) w
 WHERE rn = 1 AND LEN(Name) > 0 AND Name IS NOT NULL
 ORDER BY Name";

      using (var conn = new SqlConnection(_config.GetConnectionString("DungeonDb")))
      {
        await conn.OpenAsync();

        var results = await conn.QueryAsync<Weapon>(sql);

        return results.ToArray();
      }

      //var query = _context.Weapons.Distinct();
      //return await query.ToArrayAsync();
    }

    public async Task<Choice[]> GetChoicesByCharacterIdAsync(int id)
    {
      var character = await _context.Characters
        .Include(c => c.Choices)
        .ThenInclude(c => c.ChoiceItems)
        .Where(c => c.CharacterId == id).FirstOrDefaultAsync();

      return character.Choices.ToArray();
    }

    }
}
