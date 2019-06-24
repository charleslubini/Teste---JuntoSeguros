using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TesteJuntoSeguros.Models;
using TesteJuntoSeguros.Utils;

namespace JWT.Controllers
{
  [Route("api/[controller]")]
  public class LoginController : Controller
  {
    private readonly UserContext _context;
    private IConfiguration _config;

    public LoginController(IConfiguration config, UserContext context)
    {
      _config = config;
      _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult CreateToken([FromBody]LoginModel login)
    {
      IActionResult response = Unauthorized();
      var user = Authenticate(login);

      if (user != null)
      {
        var tokenString = BuildToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }

    [AllowAnonymous]
    [HttpPost("generate-token-change-password")]
    public IActionResult CreateTokenPassword([FromBody]ChangePasswordModel changePassword)
    {
      UserModel user = _context.users
        .Where(u => u.username == changePassword.username)
        .SingleOrDefault();

      if (user != null)
      {
        var tokenString = BuildToken(user);
        var tokenChangePassword = RandomGenerator.GenerateString(4);

        user.tokenChangePassword = RandomGenerator.CalculateHash(tokenChangePassword);
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChangesAsync();

        return Ok(new { changePasswordToken = tokenChangePassword });
      } 
      else 
      {
        return NotFound();
      }
    }

    [AllowAnonymous]
    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody]ChangePasswordModel changePassword)
    {
       UserModel user = _context.users
            .Where(u => u.username == changePassword.username && RandomGenerator.CheckMatch(u.tokenChangePassword, changePassword.tokenChangePassword))
            .SingleOrDefault();

      if (user == null || changePassword.password != changePassword.passwordConfirm)
      {
        return NotFound();
      }

      user.password = RandomGenerator.CalculateHash(changePassword.password);
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChangesAsync();

      return Ok(user);
    }

    private string BuildToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
     }

     private UserModel Authenticate(LoginModel login)
     {
        UserModel user = _context.users
            .Where(u => u.username == login.username && RandomGenerator.CheckMatch(u.password, login.password))
            .SingleOrDefault();

        return user;
     }

  }
}