using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyCodeCamp.Data;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  [ApiController]
  [Route("api/camps/{moniker}/speakers/{speakerId:int}/[controller]")]
  public class TalksController : ControllerBase
  {
    private readonly ICampRepository _repository;
    private readonly IMapper _mapper;

    public TalksController(ICampRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public IActionResult Get(string moniker, int speakerId)
    {
      var result = _repository.GetTalks(speakerId);
      if (result != null && result.First().Speaker.Camp.Moniker != moniker)
      {
        return BadRequest();
      }

      return Ok(_mapper.Map<IEnumerable<TalkModel>>(result));
    }
  }
}
