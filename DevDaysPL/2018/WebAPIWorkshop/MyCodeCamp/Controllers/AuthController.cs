using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly UserManager<CampUser> _userManager;
    private readonly IPasswordHasher<CampUser> _hasher;
    private readonly IConfiguration _config;

    public AuthController(UserManager<CampUser> userManager, 
      IPasswordHasher<CampUser> hasher,
      IConfiguration config)
    {
      _userManager = userManager;
      _hasher = hasher;
      _config = config;
    }

    [HttpPost("token")]
    public async Task<IActionResult> Post([FromBody]CredentialModel model)
    {
      // Validate the Credential
      var user = await _userManager.FindByEmailAsync(model.Username);
      if (user != null)
      {
        if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
              == PasswordVerificationResult.Success)
        {
          // Claims
          var claims = new Claim[]
          {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim("FavoriteColor", "Blue")
          };

          // Keys
          var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
          var signingCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

          // Build the Token
          var token = new JwtSecurityToken(
            issuer: _config["Tokens:Issuer"],
            audience: _config["Tokens:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signingCreds);

          return Ok(new
          {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
          });


        }
      }

      return BadRequest();
    }
  }
}
