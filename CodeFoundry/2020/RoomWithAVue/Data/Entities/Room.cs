using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomWithAVue.Data.Entities
{
  public class Room
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxGuests { get; set; }
    public double Rate { get; set; }
    public ICollection<RoomImage> Images { get;  set; }
  }
}
