using System;
using System.Collections.Generic;

namespace DungeonsAndDragons.Data.Entities
{
  public partial class CharacterClass
  {
    public int CharacterClassId { get; set; }
    public string ClassName { get; set; }
    public string SubclassName { get; set; }
    public int Level { get; set; }
  }
}
