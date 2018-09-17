using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  [ApiController]
  [Route("api/camps/{moniker}/[controller]")]
  public class SpeakersController : ControllerBase
  {
    private readonly ICampRepository _repository;
    private readonly IMapper _mapper;
    private readonly UserManager<CampUser> _userManager;

    public SpeakersController(ICampRepository repository, 
      IMapper mapper,
      UserManager<CampUser> userManager)
    {
      _repository = repository;
      _mapper = mapper;
      _userManager = userManager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SpeakerModel>> Get(string moniker)
    {
      var result = _repository.GetSpeakersByMonikerWithTalks(moniker);
      return Ok(_mapper.Map<IEnumerable<SpeakerModel>>(result));
    }

    [HttpGet("{id:int}", Name = "GetSpeaker")]
    public ActionResult<IEnumerable<SpeakerModel>> Get(string moniker, int id)
    {
      var result = _repository.GetSpeakerWithTalks(id);

      if (result.Camp.Moniker != moniker) return BadRequest();

      return Ok(_mapper.Map<SpeakerModel>(result));
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<SpeakerModel>> Post(string moniker, [FromBody] SpeakerModel model)
    {
      try
      {
        var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
        if (user != null)
        {
          var camp = _repository.GetCampByMonikerWithSpeakers(moniker);
          if (camp != null)
          {
            var speaker = _mapper.Map<Speaker>(model);
            speaker.User = user;
            camp.Speakers.Add(speaker);
            _repository.Add(speaker);

            if (await _repository.SaveAllAsync())
            {
              var url = Url.RouteUrl("GetSpeaker", new { Moniker = moniker, id = speaker.Id });
              return Created(url, _mapper.Map<SpeakerModel>(speaker));
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
      return BadRequest();
    }
  }
}
