using System.Collections.Generic;

namespace FunWithASPNETCore.Controllers
{
  public class Course
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Student> Students { get; set; }
  }
}