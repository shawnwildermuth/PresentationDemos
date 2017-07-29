using System;
using System.Collections.Generic;

namespace Aversion.Data.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }
    public string OrderNumber { get; set; }
    public ICollection<OrderItem> Items { get; set; }
  }
}