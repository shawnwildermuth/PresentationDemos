using System;
using System.Collections.Generic;

namespace DungeonsAndDragons.Data.Entities
{
  public partial class Choice
  {
    public Choice()
    {
      ChoiceItems = new HashSet<ChoiceItem>();
    }

    public int ChoiceId { get; set; }
    public string Category { get; set; }

    public ICollection<ChoiceItem> ChoiceItems { get; set; }
  }
}
