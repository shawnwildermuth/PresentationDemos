using System;
using System.Collections.Generic;

namespace DungeonsAndDragons.Data.Entities
{
  public partial class ChoiceItem
  {
    public int ChoiceItemId { get; set; }
    public string Name { get; set; }
    public int ChoiceId { get; set; }
  }
}
