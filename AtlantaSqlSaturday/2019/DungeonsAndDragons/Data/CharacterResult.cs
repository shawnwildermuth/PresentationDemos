using DungeonsAndDragons.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Data
{
    public class CharacterResult
    {
    public int Count { get; set; }
    public Character[] Results { get; set; }
    public DateTime TimeStamp { get; set; }
  }
}
