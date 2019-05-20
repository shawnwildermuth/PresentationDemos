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
  [Route("api")]
  [ApiController]
  public class CharactersController : ControllerBase
  {
    private readonly IDungeonRepository _repository;
    private readonly IConfiguration _config;

    public CharactersController(IDungeonRepository repository, IConfiguration config)
    {
      _repository = repository;
      _config = config;
    }

    [HttpGet("characters")]
    public async Task<ActionResult<Character[]>> GetAsync(string className = null)
    {
      if (className == null)
      {
        return Ok(Result(await _repository.GetCharactersAsync()));
      }
      else
      {
        return Ok(Result(await _repository.GetCharactersByClassAsync(className)));
      }
    }

    [HttpGet("characters/{id:int}")]
    public async Task<ActionResult<Character>> GetByIdAsync(int id)
    {
      return Ok(await _repository.GetCharacterAsync(id));
    }

    [HttpGet("characters/{name}")]
    public async Task<ActionResult<Character>> GetByNameAsync(string name)
    {
      return Ok(await _repository.GetCharacterByNameAsync(name));
    }

    [HttpGet("weapons/unique")]
    public async Task<ActionResult<Weapon[]>> GetWeaponNamesAsync()
    {
      return Ok(await _repository.GetUniqueWeaponNames());
    }

    private object Result(Character[] characters)
    {
      return new CharacterResult()
      {
        Count = characters.Count(),
        Results = characters,
        TimeStamp = DateTime.UtcNow
      };
    }

  }
}
