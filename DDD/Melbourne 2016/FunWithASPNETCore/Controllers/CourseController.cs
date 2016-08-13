using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunWithASPNETCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace FunWithASPNETCore.Controllers
{
  [Route("api/course")]
  public class CourseController : Controller
  {
    private FunContext _ctx;

    public CourseController(FunContext ctx)
    {
      _ctx = ctx;
    }
    
    [HttpGet("")]
    public IActionResult Get()
    {
      var courses = _ctx.Courses.ToList();

      var badThingsHappen = false;

      if (badThingsHappen == true) return BadRequest("Bad things");

      return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      return Ok(new Course()
      {
        Id = 1,
        Name = "Fun with ASPNET Core"
      });
    }
  }
}
