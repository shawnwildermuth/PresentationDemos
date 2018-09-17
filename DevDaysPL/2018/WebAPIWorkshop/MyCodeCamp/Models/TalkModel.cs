using System;

namespace MyCodeCamp.Models
{
  public class TalkModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }
    public string Category { get; set; }
    public string Level { get; set; }
    public string Prerequisites { get; set; }
    public DateTime StartingTime { get; set; } = DateTime.Now;
    public string Room { get; set; }
  }
}