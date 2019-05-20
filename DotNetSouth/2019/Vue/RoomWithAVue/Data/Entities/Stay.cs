using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomWithAVue.Data.Entities
{
  public class Stay
  {
    public int Id { get; set; }
    public Room Room { get; set; }
    public DateTime Arrival { get; set; } = DateTime.Today;
    public DateTime Departure { get; set; } = DateTime.Today;
    public Customer Guest { get; set; }
    public double Rate { get; set; }

  }
}
