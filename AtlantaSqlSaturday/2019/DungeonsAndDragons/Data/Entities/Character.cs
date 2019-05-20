using System;
using System.Collections.Generic;

namespace DungeonsAndDragons.Data.Entities
{
  public partial class Character
  {
    public Character()
    {
      Classes = new HashSet<CharacterClass>();
      Choices = new HashSet<Choice>();
      Skills = new HashSet<Skill>();
      Spells = new HashSet<Spell>();
      Weapons = new HashSet<Weapon>();
    }

    public int CharacterId { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string Background { get; set; }
    public int Level { get; set; }
    public int HitPoints { get; set; }
    public int ArmorClass { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
    public Alignment Alignment { get; set; }
    public string Location { get; set; }

    public ICollection<CharacterClass> Classes { get; set; }
    public ICollection<Choice> Choices { get; set; }
    public ICollection<Skill> Skills { get; set; }
    public ICollection<Spell> Spells { get; set; }
    public ICollection<Weapon> Weapons { get; set; }
  }
}
