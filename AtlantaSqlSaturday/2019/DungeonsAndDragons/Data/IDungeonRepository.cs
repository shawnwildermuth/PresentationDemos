using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonsAndDragons.Data.Entities;

namespace DungeonsAndDragons.Data
{
  public interface IDungeonRepository
  {
    Task<Character> GetCharacterAsync(int id);
    Task<Character> GetCharacterByNameAsync(string name);
    Task<Character[]> GetCharactersAsync();
    Task<Character[]> GetCharactersByClassAsync(string className);
    Task<Weapon[]> GetUniqueWeaponNames();
    Task<Choice[]> GetChoicesByCharacterIdAsync(int id);
  }
}