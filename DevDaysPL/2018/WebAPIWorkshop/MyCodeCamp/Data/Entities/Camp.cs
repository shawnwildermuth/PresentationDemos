using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeCamp.Data.Entities
{
  public class Camp
  {
    public int Id { get; set; }
    public string Moniker { get; set; }
    public string Name { get; set; }
    public DateTime EventDate { get; set; } = DateTime.MinValue;
    public int Length { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }

    public ICollection<Speaker> Speakers {get;set;}

    public byte[] RowVersion { get;set;}
  }
}
