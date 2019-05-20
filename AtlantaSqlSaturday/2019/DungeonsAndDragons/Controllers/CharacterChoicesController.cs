using DungeonsAndDragons.Data;
using DungeonsAndDragons.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDragons.Controllers
{
  [Route("api/characters/{id:int}/choices")]
  [ApiController]
  public class CharacterChoicesController : ControllerBase
  {
    private readonly IDungeonRepository _repository;

    public CharacterChoicesController(IDungeonRepository repository)
    {
      _repository = repository;
    }

    public async Task<ActionResult<Choice[]>> Get(int id)
    {
      var result = await _repository.GetChoicesByCharacterIdAsync(id);
      return Ok(result);
    }
  }
}
