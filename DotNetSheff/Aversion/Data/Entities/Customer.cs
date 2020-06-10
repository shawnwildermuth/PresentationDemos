using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aversion.Data.Entities
{
  public class Customer
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Contact { get; set; }
    public Address Address { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}
