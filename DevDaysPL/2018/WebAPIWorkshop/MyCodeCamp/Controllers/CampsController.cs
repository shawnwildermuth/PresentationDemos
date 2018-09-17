using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CampsController : ControllerBase
  {
    private readonly ICampRepository _repository;
    private readonly IMapper _mapper;

    public CampsController(ICampRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CampModel>> Get()
    {
      try
      {
        IEnumerable<Camp> result = _repository.GetAllCamps();

        return Ok(_mapper.Map<IEnumerable<CampModel>>(result));
      }
      catch (Exception)
      {
      }

      return BadRequest();
    }

    [HttpGet("{moniker}", Name = "GetCamp")]
    public ActionResult<CampModel> Get(string moniker, bool includeSpeakers = false)
    {
      try
      {
        Camp result;

        if (includeSpeakers) result = _repository.GetCampByMonikerWithSpeakers(moniker);
        else result = _repository.GetCampByMoniker(moniker);

        if (result == null)
        {
          return NotFound();
        }

        return _mapper.Map<CampModel>(result);
      }
      catch (Exception)
      {
      }

      return BadRequest();
    }

    [HttpPost]
    public async Task<ActionResult<CampModel>> Post([FromBody]CampModel model)
    {
      try
      {
        var camp = _mapper.Map<Camp>(model);

        _repository.Add(camp);
        if (await _repository.SaveAllAsync())
        {
          var url = Url.RouteUrl("GetCamp", new { Moniker = camp.Moniker });

          return Created(url, _mapper.Map<CampModel>(camp));
        }
      }
      catch (Exception)
      {
      }

      return BadRequest();
    }

    [HttpPut("{moniker}")]
    public async Task<ActionResult<CampModel>> Put(string moniker, [FromBody]CampModel model)
    {
      try
      {
        var oldCamp = _repository.GetCampByMoniker(moniker);
        if (oldCamp.Moniker == moniker)
        {
          // Map it
          _mapper.Map(model, oldCamp);

          if (await _repository.SaveAllAsync())
          {
            return Ok(_mapper.Map<CampModel>(oldCamp));
          }

        }
      }
      catch (Exception)
      {
      }

      return BadRequest();
    }


    [HttpDelete("{moniker}")]
    public async Task<IActionResult> Delete(string moniker)
    {
      try
      {
        var camp = _repository.GetCampByMoniker(moniker);
        _repository.Delete(camp);
        if (await _repository.SaveAllAsync())
        {
          return Ok();
        }
      }
      catch (Exception)
      {
      }

      return BadRequest();
    }
  }
}
