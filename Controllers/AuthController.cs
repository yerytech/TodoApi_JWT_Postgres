using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IConfiguration _config;

  public AuthController(AppDbContext context, IConfiguration config)
  {
    _context = context;
    _config = config;
  }
  [HttpPost("register")]
  public async Task<IActionResult> Register(UserRegisterDto request)
  {
    var exists = await _context.Users.AnyAsync(u => u.UserName == request.UserName);
    if (exists)
    {
      return BadRequest("Ese nombre de usuario ya existe");
    }

   var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = new User
    {
      UserName = request.UserName,
      Password = hashedPassword,

    };
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return Ok("Usuario registrado");
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(UserLoginDto request)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
    if (user == null)
    {
      return Unauthorized("Credenciales invalidas");

    }
    bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
    if (!isValid)
    {
      return Unauthorized("Credenciales invalidas");
  
}
    

        var secreteKey = _config["Jwk:Key"]??"estaEsUnaClaveMuySeguraDeAlMenos32Caracteres!";
    var token = TokenService.GenerateToken(user.UserName, user.Id ,secreteKey);

    return Ok( new { token });
    
  }
}